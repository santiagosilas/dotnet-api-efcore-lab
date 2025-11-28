using api.Models.One2One;

// Padrão Repository: Para desacoplar o acesso aos dados da aplicação
namespace api.Repositories
{
    public interface IContatoRepository
    {
        public Task<List<Contato>> GetAllContatos();
        public Task<List<Contato>> GetAllContatosSemEndereco();
        public Task<List<Contato>> GetAllContatosComEndereco();

        public Task<Contato?> GetContatoById(int id);

        public Task AddContato(Contato contato);

        public Task DeleteContato(Contato contato);

        public Task<List<Contato>> BuscaPaginada(int pageNumber, int pageSize, string? search = null);




    }
}