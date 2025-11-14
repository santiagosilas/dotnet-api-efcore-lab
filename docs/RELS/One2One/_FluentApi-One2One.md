```csharp
public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        . . .
            /*
                Relacionamento 1:1 - PostInfo tem um PostDetails
            */
            modelBuilder.Entity<Contato>()
                .HasOne(contato => contato.Endereco)
                .WithOne(endereco => endereco.Contato)
                .HasForeignKey<Endereco>(endereco => endereco.ContatoId) // Explicita-se com <PostDetails> que este é o lado dependente no relacionamento 1:1
                .OnDelete(DeleteBehavior.Cascade);
```
