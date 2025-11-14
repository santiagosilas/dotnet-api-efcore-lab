```csharp
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
```