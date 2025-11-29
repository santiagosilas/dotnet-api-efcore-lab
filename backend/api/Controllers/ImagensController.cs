
using api.Data;
using api.DTOs;
using api.Models.EnvioDeArquivos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Entity = api.Models.EnvioDeArquivos.Imagem;
//using DTO = api.DTOs.ImagemDTO;
//using DTOCreate = api.DTOs.ImagemDTOCreate;


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
    public class ImagensController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly DbSet<Imagem> _dbSet;
        private readonly List<string> allowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        public ImagensController(ApiContext context)
        {
            _context = context;
            _dbSet = _context.Imagens;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagemDTO>>> GetAll()
        {
            return await _dbSet
                .Select(i => new ImagemDTO {Id = i.Id, Titulo = i.Titulo, Url = i.Url  })
                .ToListAsync();
        }

        [HttpPost("SalvarImagem")]
        public async Task<IActionResult> SalvarImagem( IFormFile arquivo, string titulo)
        {
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado!");
            var fileExtension = Path.GetExtension(arquivo.FileName);

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest($"Formato de arquivo não suportado!");

            if (String.IsNullOrEmpty(titulo) && titulo.Length > 3)
                return BadRequest($"Informe um título válido!");

            try 
            {
                var nomeArquivo = $"{Guid.NewGuid()}{Path.GetExtension(arquivo.FileName)}";
                var pathDir = Path.Combine("Uploads");
                if (!Directory.Exists(pathDir))
                    Directory.CreateDirectory(pathDir);
                var filePath = Path.Combine(pathDir, nomeArquivo);
                using (var stream = System.IO.File.Create(filePath))
                {
                    await arquivo.CopyToAsync(stream);
                    await arquivo.OpenReadStream().CopyToAsync(stream);
                }

                // Salva o título e nome do arquivo no banco de dados
                var item = new Imagem { Titulo = titulo, Url = nomeArquivo};
                _dbSet.Add(item);
                await _context.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(GetAll), // o nome da ação (método) que o cliente deve chamar para recuperar o recurso recém-criado.
                new { id = item.Id },
                new ImagemDTO { Id = item.Id, Titulo = item.Titulo, Url = item.Url });

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao salvar o arquivo: {ex.Message}");
            }


        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ImagemDTO>> GetItemById(long id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return new ImagemDTO { Id = item.Id, Titulo = item.Titulo, Url = item.Url };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(long id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }



            var pathDir = Path.Combine("Uploads");
            var filePath = Path.Combine(pathDir, item.Url);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _dbSet.Remove(item);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{pageNumber}/{pageSize}")]
        public async Task<ActionResult<IEnumerable<ImagemDTO>>> GetPaginated(int pageNumber, int pageSize, string? search)
        {
            var query = _context.Imagens.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(item => item.Titulo!.Contains(search));
            
            return await query
                .Select(item => new ImagemDTO { Id = item.Id, Titulo = item.Titulo, Url = item.Url })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(IFormFile arquivo, long id, [FromForm]  ImagemDTOUpdate dto )
        {

            #region Verificações iniciais
            
            // Verificações no DTO
            if (id != dto.Id)
                return BadRequest();

            if (String.IsNullOrEmpty(dto.Titulo) && dto.Titulo?.Length > 3)
                return BadRequest($"Informe um título válido!");

            // Verificações no arquivo
            if (arquivo == null || arquivo.Length == 0)
                return BadRequest("Nenhum arquivo enviado!");
            var fileExtension = Path.GetExtension(arquivo.FileName);

            if (!allowedExtensions.Contains(fileExtension))
                return BadRequest($"Formato de arquivo não suportado!");

            // Recupera a imagem do banco de dados
            var imagem = await _dbSet.FindAsync(id);
            if (imagem == null)
                return NotFound();

            // Armazena o nome do arquivo antigo para exclusão
            string nomeArquivoAntigo = imagem.Url;

            // Atualiza o título da imagem
            imagem.Titulo = dto.Titulo!;

            #endregion

            #region Salva o novo arquivo

            // Salva o novo arquivo e atualiza o nome no banco de dados
            var nomeNovoArquivo = $"{Guid.NewGuid()}{Path.GetExtension(arquivo.FileName)}";
            var pathDir = Path.Combine("Uploads");
            if (!Directory.Exists(pathDir))
                Directory.CreateDirectory(pathDir);
            var pathNovoArquivo = Path.Combine(pathDir, nomeNovoArquivo);

            try
            {                
                using (var stream = System.IO.File.Create(pathNovoArquivo))
                {
                    await arquivo.CopyToAsync(stream);
                    await arquivo.OpenReadStream().CopyToAsync(stream);
                }

            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o arquivo: {ex.Message}");
            }
            #endregion

            #region Deleta o arquivo antigo
            try
            {
                var filePath = Path.Combine(pathDir, nomeArquivoAntigo);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }
            catch (Exception)
            {
                return BadRequest($"Erro ao deletar o arquivo anterior");
            }
            #endregion

            #region Salva as alterações no banco de dados
            try
            {
                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateConcurrencyException) when (!_dbSet.Any(item => item.Id == id))
            {

                return NotFound();
            }
            #endregion






            return NoContent();
        }


        /*



 



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
         */

    }
}