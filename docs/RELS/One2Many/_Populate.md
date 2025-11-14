```csharp
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
```