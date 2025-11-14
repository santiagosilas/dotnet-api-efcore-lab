```csharp
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

```