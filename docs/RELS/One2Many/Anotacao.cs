namespace api.Models.One2Many
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
    /// </summary>
    public class Anotacao
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;
        
        public long CadernoId { get; set; } // Chave estrangeira
        public Caderno Caderno { get; set; } = null!; // null! - nulo agora, mas não em runtime. Relacionamento obrigatório sempre existe, assim null!

    }
}