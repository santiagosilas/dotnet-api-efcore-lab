# Setup para Acesso a Dados

## Atualizar o arquivo appsettings.json com informações da string de conexão:

- Para definir strings de conexão com diferentes provedores, acesse: https://www.connectionstrings.com/
- Banco de dados postgres na nuvem (gratuito com limitações - ainda a ser explorado): https://supabase.com/database

```json
{
  "ConnectionStrings": {
    "SqliteConnection": "Data Source=lab.db",
    "PgConnection": "Host=localhost;Port=5432;Pooling=true;Database=lab;User Id=postgres;Password=admin;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Adicione as seguintes dependências via Nuget:

```shell
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.InMemory
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Swashbuckle.AspNetCore
dotnet add package BCrypt.Net-Next
```

Atualização: A versão 10 do .Net apresentou erros para o BD Postgres. Assim, vou usar a versão 9.0.
Ao instalar os pacotes, especificar a versão:  --version 9.0.

```shell
dotnet add package Microsoft.EntityFrameworkCore  --version 9.0
dotnet add package Microsoft.EntityFrameworkCore.Design  --version 9.0
dotnet add package Microsoft.EntityFrameworkCore.InMemory  --version 9.0
dotnet add package Microsoft.EntityFrameworkCore.Sqlite  --version 9.0
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL  --version 9.0
dotnet add package Microsoft.EntityFrameworkCore.Tools  --version 9.0
dotnet add package Swashbuckle.AspNetCore 
dotnet add package BCrypt.Net-Next
```

## Instalar (globalmente) a ferramenta de linha de comando do EF Core Tools

```
dotnet tool install --global dotnet-ef
```

## No arquivo Program.cs, obtenha as strings de conexão do arquivo appsettings.json:

```csharp
var builder = WebApplication.CreateBuilder(args);

string? stcnn = builder.Configuration.GetConnectionString("SqliteConnection");
string? stcnn2 = builder.Configuration.GetConnectionString("PgConnection");
Console.WriteLine($"SqliteConnection: {stcnn}");
Console.WriteLine($"PgConnection: {stcnn2}");

. . .

```
