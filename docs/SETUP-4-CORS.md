# Setup CORS

Adicionar politica de CORS (Cross-Origin Resource Sharing)
É um mecanismo de segurança implementado pelos navegadores
que controla se uma aplicação web em um domínio (por exemplo,
http://frontend.com.br) pode fazer requisições para um servidor
em outro domínio http://api.backend.com

```csharp

Adicionar a seguinte política CORs
. . .

// Adicionar politica de CORS (Cross-Origin Resource Sharing)
// É um mecanismo de segurança implementado pelos navegadores
// que controla se uma aplicação web em um domínio (por exemplo,
// http://frontend.com.br) pode fazer requisições para um servidor
// em outro domínio http://api.backend.com)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173"
                )
                  .AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();

        });
});


var app = builder.Build();

. . .
```

Adicione também a configuração _app.UseCors("AllowReactApp");_:

```csharp
app.UseHttpsRedirection();

// Permitir CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Execute novamente o backend e teste a integração com algum cliente:

```shell
dotnet watch run --launch-profile https
```

## Teste de um CLiente Console

Para testar um cliente simples, crie um projeto console application com os comandos e códigos a seguir:

```shell
dotnet new console -o client-prompt
dotnet sln add client-prompt
```

```csharp
using System.Net.Http.Json;
// using System.Net.Http.Json;

var client = new HttpClient
{
    BaseAddress = new Uri("https://localhost:7181")
};

var forecasts = await client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

Console.WriteLine("Weather Forecasts:");
foreach (var forecast in forecasts!)
{
    Console.WriteLine($"{forecast.Date:yyyy-MM-dd} - {forecast.TemperatureC}°C - {forecast.Summary}");
}

public class WeatherForecast
{
    public DateOnly Date { get; set; }
    public int TemperatureC { get; set; }
    public string? Summary { get; set; }
}
```
