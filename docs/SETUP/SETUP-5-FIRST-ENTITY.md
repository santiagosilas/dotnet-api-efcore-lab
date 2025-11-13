# Setup da Primeira Entidade

na pasta api/ crie uma subpasta _Models/_. Dentro desta pasta, crie o arquivo _Tarefa.cs_:

```csharp
namespace api.Models;

public class Tarefa
{
    public long Id { get; set; }
    public string? Nome { get; set; }
    public bool IsCompleta { get; set; }
}

```
