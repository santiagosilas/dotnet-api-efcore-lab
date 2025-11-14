```csharp
public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        . . .
            /*
            Relacionamento muitos-para-muitos PostInfo-PostTag
            */
            modelBuilder.Entity<Medico>()
                .HasMany(medico => medico.Pacientes)
                .WithMany(paciente => paciente.Medicos);
```
