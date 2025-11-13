namespace api.Models;

public class Tarefa
{
    public long Id { get; set; }
    public string? Nome { get; set; }

    public DateTime Data { get; set; }

    public bool IsCompleta { get; set; }
}