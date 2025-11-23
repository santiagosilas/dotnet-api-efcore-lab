using api.Models.One2One;
using api.DTOs;

namespace api.Services
{
    public interface IContatoService
    {
        public Task<List<ContatoDTO>> GetAllContatos();
        public Task<ContatoDTO> AddContato(ContatoDTOCreate dto);
    }
}