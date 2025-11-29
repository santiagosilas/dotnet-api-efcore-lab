using Microsoft.EntityFrameworkCore;

namespace api.Models.EnvioDeArquivos
{
    /// <summary>
    /// Entidade para representar uma Imagem que será armazenado no Backend
    /// 
    /// </summary>
    public class Imagem
    {
        public long Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
