using api.Repositories;
using api.Models.One2One;
using api.DTOs;

using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class ContatoService: IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }
        public async Task<List<ContatoDTO>> GetAllContatos()
        {
            var contatos = await _contatoRepository.GetAllContatos();

            return contatos.Select(c => new ContatoDTO {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();
        }
        public async Task<ContatoDTO> AddContato(ContatoDTOCreate dto)
        {
            var contato = new Contato { Nome = dto.Nome };
            await _contatoRepository.AddContato(contato);
            return new ContatoDTO { Id = contato.Id, Nome = contato.Nome};
        }

    }
}