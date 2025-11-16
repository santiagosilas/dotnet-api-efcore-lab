using api.Repositories;
using api.Models.One2One;

namespace api.Services
{
    public class ContatoService: IContatoService
    {
        private readonly IContatoRepository _contatoRepository;
        public ContatoService(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }
        public IEnumerable<Contato> GetAllContatos()
        {
            return _contatoRepository.GetAllContatos();
        }
    }
}