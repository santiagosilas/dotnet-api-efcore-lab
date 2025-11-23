using api.Models.One2One;

namespace api.Repositories
{
    public interface IContatoRepository
    {
        public Task<List<Contato>> GetAllContatos();

        public Task<Contato?> GetContatoById(int id);

        public Task AddContato(Contato contato);

        public Task DeleteContato(Contato contato);




    }
}