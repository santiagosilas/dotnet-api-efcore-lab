# HANDS-ON DTOs

DTO: Data Transfer Object (Objeto de Transferência de Dados)

Para controlar o que entrará e sairá da API dos modelos(entidades) do banco de dados.

Passo 1: Crie uma pasta DTOs/

Passo 2:
Implemente as seguintes classes de DTO:

TarefaDTO.cs

```csharp
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

```

TarefaDTOCreate.cs

```csharp
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
```

Passo 4: Atualize o TarefasController.cs da seguinte forma:

```csharp
. . .
using api.DTOs;

. . .

[HttpGet]
public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetTarefas()
{
    return await _context.Tarefas
        .Select(tarefa => new TarefaDTO(tarefa))
        .ToListAsync();
}
 . . .
```
