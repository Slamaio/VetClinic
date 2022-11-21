using VetClinic.Interfaces;

namespace VetClinic.Modules;

[Serializable]
public class Clinic : IClinic
{
    public Clinic()
    {
        Name = "Vet Clinic";
    }
    
    public Clinic(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public Dictionary<IAnimal, AnimalInfo> AnimalInfos { get; protected set; } = new();
    public List<IAnimal> Animals => AnimalInfos.Keys.ToList();

    public List<IClient> Clients =>
        Animals
            .Where(a => a.Owner != null)
            .Select(a => a.Owner)
            .Distinct()
            .ToList()!;

    public List<IAnimal> Find(List<IAnimal> source, Predicate<IAnimal> predicate) => source.FindAll(predicate);
    public List<IClient> Find(List<IClient> source, Predicate<IClient> predicate) => source.FindAll(predicate);

    public void AddAnimal(IAnimal animal)
    {
        AnimalInfos.Add(animal, new AnimalInfo(animal));
    }

    public void DeleteAnimal(IAnimal animal)
    {
        AnimalInfos.Remove(animal);
    }

    public void DeleteClient(IClient client)
    {
        foreach (var animal in client.Pets)
            AnimalInfos.Remove(animal);
    }

    public IReadOnlyDictionary<DateTime, DateTime?> VisitHistory(IAnimal animal, TimeSpan timeSpan = TimeSpan.All) =>
        AnimalInfos[animal].VisitRecord
            .Where(p => CheckTimeSpan(p.Key, timeSpan))
            .ToDictionary(p => p.Key, p => p.Value);

    private static bool CheckTimeSpan(DateTime date, TimeSpan span)
    {
        return span switch
        {
            TimeSpan.Day => DateTime.Now.AddDays(-1) <= date,
            TimeSpan.Week => DateTime.Now.AddDays(-7) <= date,
            TimeSpan.Month => DateTime.Now.AddMonths(-1) <= date,
            TimeSpan.Year => DateTime.Now.AddYears(-1) <= date,
            TimeSpan.All => true,
            _ => false
        };
    }

    public List<IAnimal> GetUpcoming(int days = 7) =>
        Animals
            .Where(a => a.ScheduledProcedures
                .Any(p => p.Value.AddDays(-days) <= DateTime.Now))
            .ToList();


    public void AddVisit(IAnimal animal, DateTime date)
    {
        AnimalInfos[animal].AddRecord(date);
    }

    public void AddDischarge(IAnimal animal, DateTime date)
    {
        AnimalInfos[animal].AddRecord(date);
    }

    public void ScheduleProcedure(IAnimal animal, string name, DateTime date)
    {
        animal.ScheduleProcedure(name, date);
    }

    public void PerformProcedure(IAnimal animal, string name, DateTime date)
    {
        animal.PerformProcedure(name, date);
    }
}