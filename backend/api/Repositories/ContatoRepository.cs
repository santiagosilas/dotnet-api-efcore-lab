using api.Data;
using api.Models.One2One;

using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class ContatoRepository: IContatoRepository
    {
        private readonly ApiContext _context;

        public ContatoRepository(ApiContext context)
        {
            _context = context;
        }

        public async Task<List<Contato>> GetAllContatos()
        {
            return await _context.Contatos.ToListAsync();
        }

        public async Task<Contato?> GetContatoById(int id)
        {
            return await _context.Contatos
                                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddContato(Contato contato)
        {
            _context.Contatos.Add(contato);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContato(Contato contato)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }


    }
}