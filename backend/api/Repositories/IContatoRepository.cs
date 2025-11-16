using api.Models.One2One;

namespace api.Repositories
{
    public interface IContatoRepository
    {
        public IEnumerable<Contato> GetAllContatos();
    }
}