# Setup do Swagger

Swagger: Ferramenta que gera documentação interativa automática para Web APIs, e permite visualizar e testar endpoints diretamente no navegador.

Instalar via Nuget, o pacote:

- https://www.nuget.org/packages/swashbuckle.aspnetcore

```shell
dotnet add package Swashbuckle.AspNetCore
```

# Edição do Program.cs com COnfigurações para o Swagger via pacote swashbuckle

```csharp
. . .

// Add services to the container.

builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Swagger/OpenAPI - https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
// builder.Services.AddOpenApi();  // Vem por padrão ao criar o projeto





var app = builder.Build();

. . .
```

```csharp
. . .

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();
}

. . .
```

```csharp
dotnet add package Swashbuckle.AspNetCore
```

## Execute o Projeto e acesse o swagger

Para executar o projeto:

```
dotnet watch run --launch-profile https
```

Para acessar o Swagger:

- https://localhost:7181/swagger/index.html
