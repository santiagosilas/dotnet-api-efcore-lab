namespace api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    public DbSet<Tarefa> Tarefas { get; set; } = null!;


    public void PopulateTestData()
    {
        
    }

}