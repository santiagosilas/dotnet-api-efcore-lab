```csharp
public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        . . .
            /*
            Relacionamento 1:N - PostCategory tem muitos PostInfos
            */
            modelBuilder.Entity<Caderno>()
                .HasMany(cad => cad.Anotacoes)
                .WithOne(anotacao => anotacao.Caderno)
                .HasForeignKey(anotacao => anotacao.CadernoId) // Em 1:N,a FK sempre está no lado muitos, e aí não se especifica <Entidade>
                .OnDelete(DeleteBehavior.Cascade);
```
