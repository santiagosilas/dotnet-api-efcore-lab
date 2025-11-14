namespace api.Models.One2One
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-one
    /// </summary>
    public class Endereco
    {
        public long Id { get; set; }
        public string Rua { get; set; } = string.Empty;

        public int Numero { get; set; }

        // Chave estrangeira
        public long ContatoId { get; set; }

        // Propriedade de navegação não inicializada - O EF gerenciará isso.
        public Contato Contato { get; set; } = null!;

    }
}