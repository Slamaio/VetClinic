namespace VetClinic;

public interface IClinic
{
    public string Name { get; set; }
    
    public List<IAnimal> Animals { get; }
    public Dictionary<IAnimal, AnimalInfo> AnimalInfos { get; }
    public List<IClient> Clients { get; }

    public List<IAnimal> Find(List<IAnimal> source, Predicate<IAnimal> predicate);
    public List<IClient> Find(List<IClient> source, Predicate<IClient> predicate);

    public void Add(IAnimal animal);
    
    public void Delete(IAnimal animal);
    public void Delete(IClient client);

    public Dictionary<DateTime, DateTime?> VisitHistory(IAnimal animal, TimeSpan? timeSpan);

    public List<IAnimal> GetUpcoming(int days = 7);

    public void RecordVisit(IAnimal animal, DateTime date);

    public void RecordDischarge(IAnimal animal, DateTime date);

    public void ScheduleProcedure(IAnimal animal, DateTime date, string name);
    public void PerformProcedure(IAnimal animal, string name);
}