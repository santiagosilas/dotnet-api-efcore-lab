namespace api.DTOs;

public class TarefaDTOPartialUpdate
{
    public long Id { get; set; }
    public string? Nome { get; set; }

    public DateTime? Data { get; set; }

    public bool? IsCompleta { get; set; }

    public TarefaDTOPartialUpdate()
    {

    }

}