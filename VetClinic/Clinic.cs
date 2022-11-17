namespace VetClinic;

public class Clinic : IClinic
{
    public Clinic(string name)
    {
        Name = name;
    }

    public string Name { get; set; }

    public Dictionary<IAnimal, AnimalInfo> AnimalInfos { get; } = new();
    public List<IAnimal> Animals => AnimalInfos.Keys.ToList();

    public List<IClient> Clients =>
        Animals
            .Where(a => a.Owner != null)
            .Select(a => a.Owner)
            .Distinct()
            .ToList()!;

    public List<IAnimal> Find(List<IAnimal> source, Predicate<IAnimal> predicate) => source.FindAll(predicate);
    public List<IClient> Find(List<IClient> source, Predicate<IClient> predicate) => source.FindAll(predicate);

    public void Add(IAnimal animal)
    {
        AnimalInfos.Add(animal, new AnimalInfo(animal));
    }

    public void Delete(IAnimal animal)
    {
        AnimalInfos.Remove(animal);
    }

    public void Delete(IClient client)
    {
        foreach (var animal in client.Pets)
            AnimalInfos.Remove(animal);
    }

    public Dictionary<DateTime, DateTime?> VisitHistory(IAnimal animal, TimeSpan? timeSpan = null)
    {
        if (timeSpan == null)
            return AnimalInfos[animal].VisitRecord;

        return (Dictionary<DateTime, DateTime?>)AnimalInfos[animal].VisitRecord
            .Where(v => (DateTime.Now - v.Key).Days < (int)timeSpan);
    }

    public List<IAnimal> GetUpcoming(int days = 7) =>
        Animals
            .Where(a => a.ScheduledProcedures
                .Any(p => (DateTime.Now - p.PlannedDate).Days <= days))
            .ToList();

    
    
    public void RecordVisit(IAnimal animal, DateTime date)
    {
        AnimalInfos[animal].AddVisit(date);
    }

    public void RecordDischarge(IAnimal animal, DateTime date)
    {
        AnimalInfos[animal].AddDischarge(date);
    }

    public void ScheduleProcedure(IAnimal animal, DateTime date, string name)
    {
        animal.ScheduledProcedures.Add(new Procedure
        {
            Clinic = this,
            Name = name,
            PlannedDate = date
        });
    }

    public void PerformProcedure(IAnimal animal, Procedure procedure)
    {
        // animal.
        throw new NotImplementedException();
    }
}