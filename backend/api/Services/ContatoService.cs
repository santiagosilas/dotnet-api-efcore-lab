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

        public async Task<List<ContatoDTO>> GetAllContatosSemEndereco()
        {
            var contatos = await _contatoRepository.GetAllContatosSemEndereco();

            return contatos.Select(c => new ContatoDTO
            {
                Id = c.Id,
                Nome = c.Nome
            }).ToList();
        }

        public async Task<List<ContatoComEnderecoDTO>> GetAllContatosComEndereco()
        {
            var contatos = await _contatoRepository.GetAllContatosComEndereco();

            return contatos.Select(c => new ContatoComEnderecoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Endereco = new EnderecoDTO { Rua = c.Endereco.Rua, Numero = c.Endereco.Numero, ContatoId = c.Endereco.ContatoId }
            }).ToList();
        }

        public async Task<List<ContatoComEnderecoDTO>> BuscaPaginada(int pageNumber, int pageSize, string? search = null)
        { 
            var contatos = await _contatoRepository.BuscaPaginada(pageNumber, pageSize, search);
            var data = contatos.Select(c => new ContatoComEnderecoDTO
            {
                Id = c.Id,
                Nome = c.Nome,
                Endereco = (c.Endereco != null) ? new EnderecoDTO { Rua = c.Endereco.Rua, Numero = c.Endereco.Numero, ContatoId = c.Endereco.ContatoId } : null
            });

            return data.ToList();
        }


        public async Task<ContatoDTO> AddContato(ContatoDTOCreate dto)
        {
            var contato = new Contato { Nome = dto.Nome };
            await _contatoRepository.AddContato(contato);
            return new ContatoDTO { Id = contato.Id, Nome = contato.Nome};
        }


    }
}