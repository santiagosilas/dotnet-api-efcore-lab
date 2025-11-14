namespace api.Models.Many2Many
{
    /// <summary>
    /// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/one-to-many
    /// </summary>
    public class Medico
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();

    }
}