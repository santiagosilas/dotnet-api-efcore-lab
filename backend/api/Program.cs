using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Repositories;
using api.Services;
using Microsoft.Extensions.DependencyInjection;

// Instância da aplicação web
var builder = WebApplication.CreateBuilder(args);

string? stcnn = builder.Configuration.GetConnectionString("SqliteConnection");
string? stcnn2 = builder.Configuration.GetConnectionString("PgConnection");
Console.WriteLine($"SqliteConnection: {stcnn}");
Console.WriteLine($"PgConnection: {stcnn2}");



// Add services to the container.

builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Swagger/OpenAPI - https://aka.ms/aspnetcore/swashbuckle


builder.Services.AddSwaggerGen(); // Testando o Padrão
builder.Services.AddOpenApi();  // Vem por padrão ao criar o projeto


// Adiciona o contexto do bd ao contêiner de serviços (DI - Dependency Injection)
// builder.Services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("MemoryDb"));
//builder.Services.AddDbContext<ApiContext>(opt => opt.UseSqlite(stcnn));
builder.Services.AddDbContext<ApiContext>(options => options.UseNpgsql(stcnn2));

// Registro no Container de Injeção de Dependência (DI) - Repositório e Services
builder.Services.AddScoped<IContatoRepository, ContatoRepository>();
builder.Services.AddScoped<IContatoService, ContatoService>();


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

// Habilita logs detalhados no swagger
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SupportNonNullableReferenceTypes();
//});

// habilita exceções detalhadas
//builder.Logging.AddDebug();
//builder.Logging.AddConsole();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI();
    
    // app.MapOpenApi();

    app.MapOpenApi();        // Gera o documento OpenAPI (swagger.json)
    //app.UseSwaggerUI();      // Mostra o Swagger UI
    

    app.UseSwagger();            // gera /swagger/v1/swagger.json
    app.UseSwaggerUI();          // gera UI em /swagger

    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<ApiContext>();
        context.PopulateTestData();
    }

}

app.UseHttpsRedirection();

// Permitir CORS
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();

// Acesso ao Swagger
// https://localhost:7181/swagger/index.html