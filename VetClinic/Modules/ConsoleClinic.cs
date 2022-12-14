using VetClinic.Interfaces;

namespace VetClinic.Modules;

[Serializable]
public class ConsoleClinic
{
    private IClinic _clinic;
    [NonSerialized] private Dictionary<string, ConsoleMenu> _menus = new();

    public ConsoleClinic(IClinic clinic)
    {
        _clinic = clinic;
        _menus["Main"] = new ConsoleMenu($"{_clinic.Name} Main Menu", ": ");
        _menus["Animals"] = new ConsoleMenu("Animals", ": ", _menus["Main"], true);
        _menus["VisitRecords"] = new ConsoleMenu("Visit records", ": ", _menus["Main"], true);
        _menus["VisitsHistory"] = new ConsoleMenu("Time span", ": ", _menus["VisitRecords"], true);
        _menus["Procedures"] = new ConsoleMenu("Procedures", ": ", _menus["Main"], true);
        _menus["Backup"] = new ConsoleMenu("Backup", ": ", _menus["Main"], true);

        PopulateMenus();
    }

    // ------------------------------------------------------------------------------------------------------
    // Populate menus...
    //
    private void PopulateMenus()
    {
        PopulateMainMenu();
        PopulateAnimalsMenu();
        PopulateVisitRecordsMenu();
        PopulateVisitsHistoryMenu();
        PopulateProceduresMenu();
        PopulateBackupMenu();
    }

    private void PopulateMainMenu()
    {
        _menus["Main"].Add(new ConsoleMenuItem("Animals", _menus["Animals"].Menu));
        _menus["Main"].Add(new ConsoleMenuItem("Clients", MenuClients));
        _menus["Main"].Add(new ConsoleMenuItem("Visit records", _menus["VisitRecords"].Menu));
        _menus["Main"].Add(new ConsoleMenuItem("Procedures", _menus["Procedures"].Menu));
        _menus["Main"].Add(new ConsoleMenuItem("Backup", _menus["Backup"].Menu));
    }

    private void PopulateAnimalsMenu()
    {
        _menus["Animals"].Add(new ConsoleMenuItem("Show list", ShowList));
        _menus["Animals"].Add(new ConsoleMenuItem("Add", Add));
        _menus["Animals"].Add(new ConsoleMenuItem("Remove", Remove));

        void ShowList()
        {
            Console.Clear();
            foreach (var animal in _clinic.Animals)
                Console.WriteLine(
                    $"{animal.Name} ({animal.Type}) - {(animal.HasOwner() ? $"{animal.Owner?.Name}" : "No owner")}");
        }

        void Add()
        {
            Console.Clear();

            var name = GetAnimalAttribute("name");
            var type = GetAnimalAttribute("type");
            var owner = GetAnimalOwner();

            Client? client = null;
            if (owner != null)
                client = (Client?)(_clinic.Clients.Find(c => c.Name == owner) ?? new Client(owner));
            var animal = new Animal(name, type, client);

            _clinic.AddAnimal(animal);
            Console.WriteLine("Successfully added.");
        }

        void Remove()
        {
            Console.Clear();
            var animal = GetAnimal();
            if (animal != null)
            {
                _clinic.DeleteAnimal(animal);
                Console.WriteLine("Successfully removed.");
            }
            else Console.WriteLine("Animal is not present in the database.");
        }
    }

    private void PopulateVisitRecordsMenu()
    {
        _menus["VisitRecords"].Add(new ConsoleMenuItem("Record a visit", Visit));
        _menus["VisitRecords"].Add(new ConsoleMenuItem("Record a discharge", Discharge));
        _menus["VisitRecords"].Add(new ConsoleMenuItem("Visits history", _menus["VisitsHistory"].Menu));

        void Visit()
        {
            Console.Clear();
            var animal = GetAnimal();
            if (animal != null)
            {
                _clinic.AddVisit(animal, DateTime.Now);
                Console.WriteLine("Visit recorded.");
            }
            else Console.WriteLine("Animal is not present in the database.");
        }

        void Discharge()
        {
            Console.Clear();
            var animal = GetAnimal();
            if (animal != null)
            {
                _clinic.AddDischarge(animal, DateTime.Now);
                Console.WriteLine("Discharge recorded.");
            }
            else Console.WriteLine("Animal is not present in the database.");
        }
    }

    private void PopulateVisitsHistoryMenu()
    {
        _menus["VisitsHistory"].Add(new ConsoleMenuItem("Day", () => ShowHistory(TimeSpan.Day)));
        _menus["VisitsHistory"].Add(new ConsoleMenuItem("Week", () => ShowHistory(TimeSpan.Week)));
        _menus["VisitsHistory"].Add(new ConsoleMenuItem("Month", () => ShowHistory(TimeSpan.Month)));
        _menus["VisitsHistory"].Add(new ConsoleMenuItem("Year", () => ShowHistory(TimeSpan.Year)));
        _menus["VisitsHistory"].Add(new ConsoleMenuItem("All", () => ShowHistory(TimeSpan.All)));

        void ShowHistory(TimeSpan span)
        {
            Console.Clear();

            var animal = GetAnimal();
            if (animal == null)
            {
                Console.WriteLine("Animal is not present in the database.");
                return;
            }

            foreach (var (visit, discharge) in _clinic.VisitHistory(animal, span))
                Console.WriteLine($"{visit} - {discharge}");
        }
    }

    private void PopulateProceduresMenu()
    {
        _menus["Procedures"].Add(new ConsoleMenuItem("Show Upcoming", ShowUpcoming));
        _menus["Procedures"].Add(new ConsoleMenuItem("Schedule a procedure", Schedule));
        _menus["Procedures"].Add(new ConsoleMenuItem("Mark as performed", Perform));

        void ShowUpcoming()
        {
            Console.Clear();
            Console.Write("Days: ");
            var days = int.Parse(Console.ReadLine());
            foreach (var animal in _clinic.GetUpcoming(days))
            {
                Console.WriteLine(
                    $"{animal.Name} ({animal.Type}) - {(animal.HasOwner() ? $"{animal.Owner?.Name}" : "No owner")}");
                var procedures = animal.ScheduledProcedures
                    .Where(p => (DateTime.Now - p.Value).Days <= days)
                    .ToDictionary(p => p.Key, p => p.Value);
                ;
                foreach (var (name, time) in procedures)
                    Console.WriteLine($"\t{name} - {time}");
            }
        }

        void Schedule()
        {
            Console.Clear();
            var animal = GetAnimal();
            if (animal == null)
            {
                Console.WriteLine("Animal is not present in the database.");
                return;
            }

            Console.Clear();
            Console.Write("Procedure name: ");
            var name = Console.ReadLine();

            Console.Write("Procedure planned date (mm/dd/yyyy hh:mm): ");
            var date = Console.ReadLine() + ":00";

            _clinic.ScheduleProcedure(animal, name, DateTime.Parse(date));

            Console.WriteLine("Successfully scheduled.");
        }

        void Perform()
        {
            Console.Clear();
            var animal = GetAnimal();
            if (animal == null)
            {
                Console.WriteLine("Animal is not present in the database.");
                return;
            }

            Console.Clear();
            Console.Write("Procedure name: ");
            var name = Console.ReadLine();

            Console.Write("Procedure planned date (mm/dd/yyyy hh:mm): ");
            var date = Console.ReadLine() + ":00";

            _clinic.PerformProcedure(animal, name, DateTime.Parse(date));

            Console.WriteLine("Successfully performed.");
        }
    }

    private void PopulateBackupMenu()
    {
        _menus["Backup"].Add(new ConsoleMenuItem("Save", Save));
        _menus["Backup"].Add(new ConsoleMenuItem("Load", Load));

        void Load()
        {
            Console.Clear();

            Console.Write("Enter the path\n> ");
            var path = Console.ReadLine();

            try
            {
                _clinic = ClinicBackup.Import(path ?? throw new InvalidOperationException());
                // _clinic.Name = temp.Name;
                // AnimalInfos = temp.AnimalInfos;
                Console.WriteLine("Backup loaded.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Load failed.");
            }
        }

        void Save()
        {
            Console.Clear();

            Console.Write("Enter the path (empty for auto)\n> ");
            var path = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(path))
                path = null;
            try
            {
                ClinicBackup.Export(_clinic, path);
            }
            catch (Exception e)
            {
                Console.WriteLine("Backup failed.");
            }

            Console.WriteLine("Backup saved.");
        }
    }
    //
    // Populate menus end.
    // ------------------------------------------------------------------------------------------------------

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
            ? _clinic.Animals.Find(a => a.Name == name && a.Type == type && !a.HasOwner())
            : _clinic.Animals.Find(a => a.Name == name && a.Type == type && a.Owner?.Name == owner);
        return animal;
    }


    private void MenuClients()
    {
        Console.Clear();
        foreach (var client in _clinic.Clients)
            Console.WriteLine(client.Name);
    }

    public void MainLoop()
    {
        _menus["Main"].Menu();
    }
}