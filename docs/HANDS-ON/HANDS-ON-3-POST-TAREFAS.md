```csharp
        [HttpPost]
        public async Task<ActionResult<TarefaDTO>> AddTarefa(TarefaDTOCreate dto)
        {
            var item = new Tarefa
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
                new TarefaDTO(item));
        }
```
