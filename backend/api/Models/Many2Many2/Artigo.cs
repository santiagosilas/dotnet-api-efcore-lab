namespace api.Models.Many2Many2
{
    public class Artigo
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public ICollection<Revisao> Revisoes { get; set; } = null!;

    }
}