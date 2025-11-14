namespace api.Models.Many2Many2
{
    /// <summary>
    /// Many to Many Relationship between PostInfo and PostTag
    /// https://www.learnentityframeworkcore.com/configuration/many-to-many-relationship-configuration
    /// </summary>
    public class Revisao
    {
        public long ArtigoId { get; set; }
        public long RevisorId { get; set; }
        public double Nota { get; set; } = 0.0;
        public Artigo Artigo { get; set; } = null!;
        public Revisor Revisor { get; set; } = null!;

    }
}