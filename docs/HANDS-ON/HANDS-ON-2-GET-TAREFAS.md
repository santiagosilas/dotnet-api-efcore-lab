Atualize o TarefasController.cs da seguinte forma:

```csharp
. . .
using api.DTOs;

. . .

[HttpGet]
public async Task<ActionResult<IEnumerable<TarefaDTO>>> GetAll()
{
    return await _context.Tarefas
        .Select(tarefa => new TarefaDTO(tarefa))
        .ToListAsync();
}
 . . .
```
