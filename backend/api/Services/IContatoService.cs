using api.Models.One2One;

namespace api.Services
{
    public interface IContatoService
    {
        public IEnumerable<Contato> GetAllContatos();
    }
}