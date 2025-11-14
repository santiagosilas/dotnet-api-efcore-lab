namespace api.Models.One2Many
{
    public class Caderno
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        // Propriedade de navegação - ICollection sempre inicializada
        public ICollection<Anotacao> Anotacoes { get; set; } = new List<Anotacao>();

    }
}