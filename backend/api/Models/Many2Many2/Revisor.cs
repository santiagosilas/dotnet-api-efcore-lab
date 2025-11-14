namespace api.Models.Many2Many2
{
    public class Revisor
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<Revisao> Revisoes { get; set; } = null!;
    }
}