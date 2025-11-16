using api.Data;
using api.Models.One2One;

namespace api.Repositories
{
    public class ContatoRepository: IContatoRepository
    {
        private readonly ApiContext _context;

        public ContatoRepository(ApiContext context)
        {
            _context = context;
        }

        public IEnumerable<Contato> GetAllContatos()
        {
            return _context.Contatos.ToList();
        }
    }
}