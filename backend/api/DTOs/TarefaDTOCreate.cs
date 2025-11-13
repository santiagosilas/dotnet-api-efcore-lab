namespace api.DTOs;

public class TarefaDTOCreate
{
    public string? Nome { get; set; }

    public DateTime Data { get; set; }

    public bool IsCompleta { get; set; }

    public TarefaDTOCreate()
    {

    }

}