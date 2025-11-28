
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Models;
using api.Data;
using api.DTOs;

using Entity = api.Models.Tarefa;
using DTO = api.DTOs.TarefaDTO;
using DTOCreate = api.DTOs.TarefaDTOCreate;

namespace api.Controllers
{
    /// <summary>
    /// Uma classe Controller fornece métodos que correspondem
    /// a requisições HTTP criadas para a aplicação ASP.Net Core
    /// Respostas são feitas por métodos "action"
    /// Sufixo Controller - O Framework busca por este sufixo
    /// Objeto ActionResult é o retorno de uma ação (action)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly DbSet<Entity> _dbSet;
        public TarefasController(ApiContext context)
        {
            _context = context;
            _dbSet = _context.Tarefas;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAll()
        {
            return await _dbSet
                .Select(tarefa => new TarefaDTO(tarefa))
                .ToListAsync();
        }

        [HttpGet("AllCompleted")]
        public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAllCompleted()
        {
            return await _dbSet
                .Select(tarefa => new TarefaDTO(tarefa))
                .Where(tarefa => tarefa.IsCompleta)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<DTO>> Add(DTOCreate dto)
        {
            var item = new Entity 
            {
                Nome = dto.Nome,
                Data = dto.Data,
                IsCompleta = dto.IsCompleta
            };

            _dbSet.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetAll), // o nome da ação (método) que o cliente deve chamar para recuperar o recurso recém-criado.
                new { id = item.Id },
                new DTO(item));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DTO>> GetItemById(long id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new DTO(item);
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<DTO>>> GetPaginated(int pageNumber, int pageSize)
        {
            return await _dbSet
                .Select(item => new DTO(item))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        } 

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, DTO dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var item = await _dbSet.FindAsync(id);
            if (item == null)
                return NotFound();


            item.Nome = dto.Nome!;
            item.IsCompleta = dto.IsCompleta;
            item.Data = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdateUser(long id, TarefaDTOPartialUpdate dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var item = await _dbSet.FindAsync(id);
            if (item == null)
                return NotFound();

            if (dto.Nome is not null)
                item.Nome = dto.Nome;
            if (dto.Data is not null)
                item.Data = (DateTime) dto.Data;
            if (dto.IsCompleta is not null)
                item.IsCompleta = (bool) dto.IsCompleta;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }        

        private bool ItemExists(long id)
        {
            return _dbSet.Any(item => item.Id == id);
        }


    }
}