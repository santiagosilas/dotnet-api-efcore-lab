```csharp
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
```