namespace api.Models.Many2Many
{
    public class Paciente
    {
        public long Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Relac. N:N com PostInfo automático
        public ICollection<Medico> Medicos { get; set; } = new List<Medico>();

    }
}