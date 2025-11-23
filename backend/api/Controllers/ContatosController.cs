
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Models;
using api.Data;
using api.DTOs;

using Entity = api.Models.Tarefa;
using DTO = api.DTOs.TarefaDTO;
using DTOCreate = api.DTOs.TarefaDTOCreate;

using api.Services;
using api.Models.One2One;

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
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _service;
        public ContatosController(IContatoService service)
        {
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDTO>>> GetAll()
        {
            var contatos = await this._service.GetAllContatos();
            return Ok(contatos);
        }

        [HttpPost]
        public async Task<ActionResult<ContatoDTO>> Create(ContatoDTOCreate dto)
        {
            var novoContato = await _service.AddContato(dto);
            return Ok(novoContato);
        }


    }
}