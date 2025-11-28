namespace api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

using api.Models.One2One;
using api.Models.One2Many;
using api.Models.Many2Many;
using api.Models.Many2Many2;

/// <summary>
/// Classe de contexto do Entity Framework Core para a API
/// Para definir o mapeamento das entidades para as tabelas do banco de dados
/// Representa uma sessão com o BD
/// </summary>
public class ApiContext : DbContext
{
    /// <summary>
    /// Para configurar o contexto na classe base
    /// </summary>
    /// <param name="options"></param>
    public ApiContext(DbContextOptions<ApiContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            /*
                Relacionamento 1:1 - PostInfo tem um PostDetails
            */
            modelBuilder.Entity<Contato>()
                .HasOne(contato => contato.Endereco)
                .WithOne(endereco => endereco.Contato)
                .HasForeignKey<Endereco>(endereco => endereco.ContatoId) // Explicita-se com <PostDetails> que este é o lado dependente no relacionamento 1:1
                .OnDelete(DeleteBehavior.Cascade);

            /*
            Relacionamento 1:N - PostCategory tem muitos PostInfos
            */
            modelBuilder.Entity<Caderno>()
                .HasMany(cad => cad.Anotacoes)
                .WithOne(anotacao => anotacao.Caderno)
                .HasForeignKey(anotacao => anotacao.CadernoId) // Em 1:N,a FK sempre está no lado muitos, e aí não se especifica <Entidade>
                .OnDelete(DeleteBehavior.Cascade);

            /*
            Relacionamento muitos-para-muitos PostInfo-PostTag
            */
            modelBuilder.Entity<Medico>()
                .HasMany(medico => medico.Pacientes)
                .WithMany(paciente => paciente.Medicos);

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

    }

    /// <summary>
    /// DbSet<T>: COleção de entidades no contexto
    /// </summary>

    public DbSet<Tarefa> Tarefas { get; set; } = null!;

    // Entidades com Relacionamento 1:1
    public DbSet<Contato> Contatos { get; set; } = null!;
    public DbSet<Endereco> Enderecos { get; set; } = null!;

    // Entidades com Relacionamento 1:N
    public DbSet<Caderno> Cadernos { get; set; } = null!;
    public DbSet<Anotacao> Anotacoes { get; set; } = null!;

    // Entidades com Relacionamento N:N (Forma Simples)
    public DbSet<Medico> Medicos { get; set; } = null!;
    public DbSet<Paciente> Pacientes { get; set; } = null!;

    // Entidades com Relacionamento N:N (Forma Avançada - Com Entidade de Associação)
    public DbSet<Artigo> Artigos { get; set; } = null!;
    public DbSet<Revisor> Revisores { get; set; } = null!;
    public DbSet<Revisao> Revisoes { get; set; } = null!;


    public void PopulateTestData()
    {
        Console.WriteLine("Apago dados de teste...");
        Tarefas.RemoveRange(Tarefas.ToList());
        Console.WriteLine("Adicionar dados de teste...");
        Tarefas.AddRange(
                new Tarefa { Nome = "Tarefa 1", Data = DateTime.UtcNow, IsCompleta = false },
                new Tarefa { Nome = "Tarefa 2", Data = DateTime.UtcNow.AddDays(1), IsCompleta = true },
                new Tarefa { Nome = "Tarefa 3", Data = DateTime.UtcNow.AddDays(2), IsCompleta = false }
        );
        SaveChanges();
        foreach (var tarefa in Tarefas)
        {
            Console.WriteLine($"Tarefa adicionada: {tarefa.Id} - {tarefa.Nome} - {tarefa.Data} - {tarefa.IsCompleta}");
        }

        // Criação de Entidades One2One de teste
        Console.WriteLine("Apago dados de teste One2One...");
        Enderecos.RemoveRange(Enderecos.ToList());
        Contatos.RemoveRange(Contatos.ToList());
        Console.WriteLine("Adicionar dados de teste One2One...");
        var contato1 = new Contato { Nome = "João Silva"};
        var endereco1 = new Endereco { Rua = "Rua A", Numero = 123, Contato = contato1 };
        Contatos.Add(contato1);
        Enderecos.Add(endereco1);
        SaveChanges();
        Console.WriteLine($"Contato adicionado: {contato1.Id} - {contato1.Nome} - Endereço: {endereco1.Rua}, {endereco1.Numero} - {endereco1.Rua}");

        // Adiciona-se mais exemplos de contatos
        string[] nomes1 = new string[]
        {
            "Ana Beatriz",
            "Bruno Silva",
            "Carla Mendes",
            "Daniel Rocha",
            "Eduarda Lima",
            "Felipe Santos",
            "Gabriela Torres",

        };
        foreach (var nome in nomes1) 
        {
            var ct = new Contato { Nome = nome,  Endereco = null };
            Contatos.Add(ct);
            SaveChanges();
        }
        string[] nomes2 = new string[]
        {
            "Henrique Alves",
            "Isabela Souza",
            "João Pedro",
            "Karen Matos",
            "Lucas Andrade",
            "Mariana Costa",
            "Nicolas Ferreira",
            "Otávio Ribeiro"
        };
        foreach (var nome in nomes2)
        {
            var ct = new Contato { Nome = nome, Endereco = null };
            Contatos.Add(ct);
            var e = new Endereco { Rua = "Rua A", Numero = 123, Contato = ct };
            Contatos.Add(ct);
            Enderecos.Add(e);

            SaveChanges();
        }


        // Criação de Entidades One2Many de teste
        Console.WriteLine("Apago dados de teste One2Many...");
        Anotacoes.RemoveRange(Anotacoes.ToList());
        Cadernos.RemoveRange(Cadernos.ToList());
        Console.WriteLine("Adicionar dados de teste One2Many...");
        var caderno1 = new Caderno { Titulo = "Caderno de Estudos" };
        var anotacao1 = new Anotacao { Descricao = "Estudar EF Core", Caderno = caderno1 };
        var anotacao2 = new Anotacao { Descricao = "Revisar C#", Caderno = caderno1 };
        Cadernos.Add(caderno1);
        Anotacoes.AddRange(anotacao1, anotacao2);
        SaveChanges();
        Console.WriteLine($"Caderno adicionado: {caderno1.Id} - {caderno1.Titulo} - Anotações: {anotacao1.Descricao}, {anotacao2.Descricao}");

        // Criação de Entidades Many2Many de teste
        Console.WriteLine("Apago dados de teste Many2Many...");
        Medicos.RemoveRange(Medicos.ToList());
        Pacientes.RemoveRange(Pacientes.ToList());
        Console.WriteLine("Adicionar dados de teste Many2Many...");
        var medico1 = new Medico { Nome = "Dr. Carlos" };
        var paciente1 = new Paciente { Nome = "Ana" };
        var paciente2 = new Paciente { Nome = "Bruno" };
        medico1.Pacientes.Add(paciente1);
        medico1.Pacientes.Add(paciente2);
        Medicos.Add(medico1);
        SaveChanges();
        Console.WriteLine($"Médico adicionado: {medico1.Id} - {medico1.Nome} - Pacientes: {string.Join(", ", medico1.Pacientes.Select(p => p.Nome))}");
        var medico2 = new Medico { Nome = "Dr. José" };
        paciente1.Medicos.Add(medico2);
        paciente2.Medicos.Add(medico2);
        Medicos.Add(medico2);
        SaveChanges();
        Console.WriteLine($"Médico adicionado: {medico2.Id} - {medico2.Nome} - Pacientes: {string.Join(", ", medico2.Pacientes.Select(p => p.Nome))}");

        // Criação de Entidades Many2Many2 de teste
        Console.WriteLine("Apago dados de teste Many2Many2...");
        Revisoes.RemoveRange(Revisoes.ToList());
        Artigos.RemoveRange(Artigos.ToList());
        Revisores.RemoveRange(Revisores.ToList());
        Console.WriteLine("Adicionar dados de teste Many2Many2...");
        var artigo1 = new Artigo { Titulo = "Introdução ao EF Core" };
        var revisor1 = new Revisor { Nome = "Mariana" };
        var revisor2 = new Revisor { Nome = "Pedro" };
        var revisao1 = new Revisao { Artigo = artigo1, Revisor = revisor1 };
        var revisao2 = new Revisao { Artigo = artigo1, Revisor = revisor2 };
        Artigos.Add(artigo1);
        Revisores.AddRange(revisor1, revisor2);
        Revisoes.AddRange(revisao1, revisao2);
        SaveChanges();
        Console.WriteLine($"Artigo adicionado: {artigo1.Id} - {artigo1.Titulo} - Revisores: {string.Join(", ", artigo1.Revisoes.Select(r => r.Revisor.Nome))}");
        


    }

}