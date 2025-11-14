namespace api.Models.One2One
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one
    /// </summary>
    public class Contato
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public Endereco? Endereco { get; set; } // Pode ser nulo (pode não existir) - relac 1:1 opcional

    }
}