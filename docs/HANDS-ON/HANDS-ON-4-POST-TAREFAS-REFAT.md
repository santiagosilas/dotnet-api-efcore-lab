```csharp
using Entity = api.Models.Tarefa;
using DTO = api.DTOs.TarefaDTO;
using DTOCreate = api.DTOs.TarefaDTOCreate;


namespace api.Controllers
{
        private readonly ApiContext _context;
        private readonly DbSet<Entity> _dbSet;
        public TarefasController(ApiContext context)
        {
            _context = context;
            _dbSet = _context.Tarefas;
        }
        . . .
        . . .
```

```csharp
        [HttpPost]
        public async Task<ActionResult<DTO>> Add(DTOCreate dto)
        {
            var item = new Entity
            {
                Nome = dto.Nome,
                Data = dto.Data,
                IsCompleta = dto.IsCompleta
            };

            _context.Tarefas.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTarefas),
                new { id = item.Id },
                new DTO(item));
        }
```
