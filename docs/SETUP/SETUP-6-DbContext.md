# Setup da Classe de Contexto

Crie uma pasta _Data/_. Nesta pasta, crie o arquivo ApiContext.cs:

```csharp
namespace api.Data;
using Microsoft.EntityFrameworkCore;
using api.Models;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<Tarefa> Tarefas { get; set; } = null!;
}
```

Em seguida, deve ser feito o registro do contexto de banco de dados em Program.cs:

Para criação de um banco de dados em memória (útil durante o aprendizado):

```csharp
// Adiciona o contexto do bd ao contêiner de serviços (DI - Dependency Injection)
builder.Services.AddDbContext<ApiContext>(opt =>
    opt.UseInMemoryDatabase("MemoryDb"));
```

Para criação de um banco de dados com SQLite:

```csharp
// Adiciona o contexto do bd ao contêiner de serviços (DI - Dependency Injection)
builder.Services.AddDbContext<ApiContext>(opt => opt.UseSqlite(stcnn));
```

Para criação de um banco de dados com Postgres:

```csharp
// Adiciona o contexto do bd ao contêiner de serviços (DI - Dependency Injection)
builder.Services.AddDbContext<ApiContext>(opt => opt.UseNpgsql(stcnn2));
```

## Criar a primeira Migration (para um BD físico)

```shell
dotnet ef migrations add InitialCreate
(uma pasta chamada Migrations será criada)
```

## Aplica a migração para criação do banco (PostgresSQL db, SQLite)

```
dotnet ef database update
```

(O arquivo lab.db será criado, caso o banco seja SQLite)

# Se ocorreram alterações no banco, crie uma nova migração

```shell
dotnet ef migrations add AdicionaCampoData
dotnet ef database update
```

## Popular o Banco com "Dados Mockados"

Em Program.cs, adicione o seguinte código dentro do condicional a seguir:

```csharp
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
        context.PopulateTestData(); // A ser implementado a seguir ...
    }
}
```

Adicione o método .PopulateTestData() à classe de contexto:

```csharp

```
