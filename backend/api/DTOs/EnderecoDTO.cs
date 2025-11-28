using api.Models.One2One;

namespace api.DTOs
{
    public class EnderecoDTO
    {
        public long Id { get; set; }

        public string Rua { get; set; } = string.Empty;

        public int Numero { get; set; }

        public long ContatoId { get; set; }
    }
}
