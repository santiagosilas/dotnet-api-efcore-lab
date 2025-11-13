namespace api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    public DbSet<Tarefa> Tarefas { get; set; } = null!;

    public void PopulateTestData()
    {
        Console.WriteLine("Apago dados de teste...");
        Tarefas.RemoveRange(Tarefas.ToList());
        Console.WriteLine("Adicionar dados de teste...");
        Tarefas.AddRange(
                new Tarefa { Nome = "Tarefa 1", Data = DateTime.Now, IsCompleta = false },
                new Tarefa { Nome = "Tarefa 2", Data = DateTime.Now.AddDays(1), IsCompleta = true },
                new Tarefa { Nome = "Tarefa 3", Data = DateTime.Now.AddDays(2), IsCompleta = false }
        );
        SaveChanges();
        foreach (var tarefa in Tarefas)
        {
            Console.WriteLine($"Tarefa adicionada: {tarefa.Id} - {tarefa.Nome} - {tarefa.Data} - {tarefa.IsCompleta}");
        }
    }

}