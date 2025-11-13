# Setup do Primeiro Controller na Api

Na pasta Controllers/, crie o arquivo TarefasController:

```csharp

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using api.Models;
using api.Data;

namespace api.Controllers
{
    /// <summary>
    /// Uma classe Controller fornece métodos que correspondem
    /// a requisições HTTP criadas para a aplicação ASP.Net Core
    /// Respostas são feitas por métodos "action"
    /// Sufixo Controller - O Framework busca por este sufixo
    /// Objeto ActionResult é o retorno de uma ação (action)
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TarefasController : ControllerBase
    {
        private readonly ApiContext _context;
        public TarefasController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetUsers()
        {
            return await _context.Tarefas
                .Select(tarefa => tarefa)
                .ToListAsync();
        }

    }
}
```

Agora, atualize o Swagger:

- https://localhost:7181/swagger
