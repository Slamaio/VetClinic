namespace VetClinic;

[Serializable]
public class ConsoleClinic : Clinic
{
    public ConsoleClinic(string name) : base(name)
    {
    }

    private void MenuAnimals()
    {
        Console.Clear();
        foreach (var animal in Animals)
            Console.WriteLine(
                $"{animal.Name} ({animal.Type}) - {(animal.HasOwner() ? $"{animal.Owner?.Name}" : "No owner")}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuClients()
    {
        Console.Clear();
        foreach (var client in Clients)
            Console.WriteLine(client.Name);
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuAdd()
    {
        Console.Clear();

        var name = GetAnimalAttribute("name");
        var type = GetAnimalAttribute("type");
        var owner = GetAnimalOwner();

        Client? client = null;
        if (owner != null)
            client = (Client?)(Clients.Find(c => c.Name == owner) ?? new Client(owner));
        var animal = new Animal(name, type, client);

        AddAnimal(animal);
        Console.WriteLine("Successfully added.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private string GetAnimalAttribute(string attribute)
    {
        Console.Write($"Animal {attribute} (blank if unknown): ");
        var value = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(value))
            value = $"Unknown {attribute}";
        return value;
    }

    private string? GetAnimalOwner()
    {
        Console.Write("Owner name (blank for no owner): ");
        var owner = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(owner))
            owner = null;
        return owner;
    }

    private IAnimal? GetAnimal()
    {
        var name = GetAnimalAttribute("name");
        var type = GetAnimalAttribute("type");
        var owner = GetAnimalOwner();

        var animal = owner == null
            ? Animals.Find(a => a.Name == name && a.Type == type && !a.HasOwner())
            : Animals.Find(a => a.Name == name && a.Type == type && a.Owner?.Name == owner);
        return animal;
    }

    private void MenuRemove()
    {
        Console.Clear();
        var animal = GetAnimal();
        if (animal != null) DeleteAnimal(animal);
        Console.WriteLine("Successfully removed.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuVisit()
    {
        Console.Clear();
        var animal = GetAnimal();
        if (animal != null) AddVisit(animal, DateTime.Now);
        Console.WriteLine("Visit recorded.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuDischarge()
    {
        Console.Clear();
        var animal = GetAnimal();
        if (animal != null) AddDischarge(animal, DateTime.Now);
        Console.WriteLine("Discharge recorded.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuUpcoming()
    {
        Console.Clear();
        Console.Write("Days: ");
        var days = Console.Read();
        foreach (var animal in GetUpcoming(days))
        {
            Console.WriteLine(
                $"{animal.Name} ({animal.Type}) - {(animal.HasOwner() ? $"{animal.Owner?.Name}" : "No owner")}");
            var procedures = animal.ScheduledProcedures.Where(p => (DateTime.Now - p.Value).Days <= days);
            foreach (var (name, time) in procedures)
                Console.WriteLine($"\t{name} - {time}");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuSchedule()
    {
        throw new NotImplementedException();
    }

    private void MenuPerform()
    {
        throw new NotImplementedException();
    }

    private void MenuHistory()
    {
        Console.Clear();

        var animal = GetAnimal();
        Console.Clear();
        Console.WriteLine("Choose the time period (blank for all):\n" +
                          "1: Day\n" +
                          "2: Week\n" +
                          "3: Month\n" +
                          "4: Year");
        int.TryParse(Console.ReadLine(), out int opt);
        var ts = opt switch
        {
            1 => TimeSpan.Day,
            2 => TimeSpan.Week,
            3 => TimeSpan.Month,
            4 => TimeSpan.Year,
            _ => TimeSpan.All
        };

        foreach (var (visit, discharge) in VisitHistory(animal, ts))
            Console.WriteLine($"{visit} - {discharge}");
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuExport()
    {
        Console.Clear();
        
        Console.Write("Enter the path (empty for auto)\n> ");
        var path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
            path = null;
        ClinicBackup.Export(this, path);

        Console.WriteLine("Backup saved.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    private void MenuImport()
    {
        Console.Clear();
        
        Console.Write("Enter the path\n> ");
        var path = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Load failed.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            return;
        }
        var temp = ClinicBackup.Import(path);
        Name = temp.Name;
        AnimalInfos = temp.AnimalInfos;

        Console.WriteLine("Backup loaded.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }

    public void MainLoop()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"== {Name} ==\n" +
                              "1: Animals\n" +
                              "2: Clients\n" +
                              "3: Add animal\n" +
                              "4: Remove animal\n" +
                              "5: Record a visit\n" +
                              "6: Record a discharge\n" +
                              "7: Get upcoming procedures\n" +
                              "8: Schedule a procedure\n" +
                              "9: Perform a procedure\n" +
                              "10: Visit history\n" +
                              "11: Save backup\n" +
                              "12: Load backup\n" +
                              "0: Quit");
            var opt = int.Parse(Console.ReadLine());
            switch (opt)
            {
                case 1:
                    MenuAnimals();
                    break;
                case 2:
                    MenuClients();
                    break;
                case 3:
                    MenuAdd();
                    break;
                case 4:
                    MenuRemove();
                    break;
                case 5:
                    MenuVisit();
                    break;
                case 6:
                    MenuDischarge();
                    break;
                case 7:
                    MenuUpcoming();
                    break;
                case 8:
                    MenuSchedule();
                    break;
                case 9:
                    MenuPerform();
                    break;
                case 10:
                    MenuHistory();
                    break;
                case 11:
                    MenuExport();
                    break;
                case 12:
                    MenuImport();
                    break;
                case 0:
                    return;
            }
        }
    }
}