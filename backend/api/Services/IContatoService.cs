using api.Models.One2One;
using api.DTOs;

namespace api.Services
{
    public interface IContatoService
    {
        public Task<List<ContatoDTO>> GetAllContatos();

        public Task<List<ContatoDTO>> GetAllContatosSemEndereco();

        public Task<List<ContatoComEnderecoDTO>> GetAllContatosComEndereco();

        public Task<List<ContatoComEnderecoDTO>> BuscaPaginada(int pageNumber, int pageSize, string? search = null);

        public Task<ContatoDTO> AddContato(ContatoDTOCreate dto);
    }
}