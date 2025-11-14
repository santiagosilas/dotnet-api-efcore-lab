```csharp
public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        . . .
            /*
                Relacionamento muitos-para-muitos Artigo-PostTag
            */
            // Define a chave primária composta
            modelBuilder.Entity<Revisao>()
                    .HasKey(rev => new { rev.ArtigoId, rev.RevisorId });

            // Relacionamento com Artigo
            modelBuilder.Entity<Revisao>()
                .HasOne(rev => rev.Artigo)
                .WithMany(artigo => artigo.Revisoes)
                .HasForeignKey(rev => rev.ArtigoId)
                .OnDelete(DeleteBehavior.Cascade);
            // Relacionamento com PostReview
            modelBuilder.Entity<Revisao>()
                .HasOne(rev => rev.Revisor)
                .WithMany(revisor => revisor.Revisoes)
                .HasForeignKey(rev => rev.RevisorId)
                .OnDelete(DeleteBehavior.Cascade);
```
