using api.Data;
using api.Models.One2One;

using Microsoft.EntityFrameworkCore;

// Padrão Repository: Para desacoplar o acesso aos dados da aplicação
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


        public async Task<List<Contato>> GetAllContatosSemEndereco()
        {
            var contatos = await _context.Contatos
                .Include(c => c.Endereco) // Eager Loading (JOIN para Trazer os Dados das Tabelas Relacionadas)
                .Where(contato => contato.Endereco == null)
                .ToListAsync();
            return contatos;
        }
        public async Task<List<Contato>> GetAllContatosComEndereco()
        {
             
            var contatos = await _context.Contatos
                .Include(c => c.Endereco)  // Eager Loading (JOIN para Trazer os Dados das Tabelas Relacionadas)
                .Where(contato => contato.Endereco != null)
                .ToListAsync();
            return contatos;
        }

        public async Task<List<Contato>> BuscaPaginada(int pageNumber, int pageSize, string? search = null ) 
        {
            var query = _context.Contatos.Select(c => c);

            if (search != null)
                query = query.Where(c => c.Nome.Contains(search));
            
            var data = await query    
                .Include(c => c.Endereco)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return data;
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