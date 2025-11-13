var builder = WebApplication.CreateBuilder(args);

string? stcnn = builder.Configuration.GetConnectionString("SqliteConnection");
string? stcnn2 = builder.Configuration.GetConnectionString("PgConnection");
Console.WriteLine($"SqliteConnection: {stcnn}");
Console.WriteLine($"PgConnection: {stcnn2}");



// Add services to the container.

builder.Services.AddControllers();

// Configuração do Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer(); // Swagger/OpenAPI - https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();
// builder.Services.AddOpenApi();  // Vem por padrão ao criar o projeto

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
