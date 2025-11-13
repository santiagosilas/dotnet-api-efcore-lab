namespace api.DTOs;
using api.Models;

public class TarefaDTO
{
    public long Id { get; set; }
    public string? Nome { get; set; }

    public bool IsCompleta { get; set; }

    public TarefaDTO(Tarefa tarefa)
    {
        Id = tarefa.Id;
        Nome = tarefa.Nome;
        IsCompleta = tarefa.IsCompleta;
    }

    public TarefaDTO()
    {

    }

}