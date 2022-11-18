namespace VetClinic;

public interface IClinic
{
    public string Name { get; set; }
    
    public List<IAnimal> Animals { get; }
    public Dictionary<IAnimal, AnimalInfo> AnimalInfos { get; }
    public List<IClient> Clients { get; }

    public List<IAnimal> Find(List<IAnimal> source, Predicate<IAnimal> predicate);
    public List<IClient> Find(List<IClient> source, Predicate<IClient> predicate);

    /// <summary>
    /// Adds a new animal to the clinic registry.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object to be added.</param>
    public void Add(IAnimal animal);
    
    /// <summary>
    /// Deletes an animal from the clinic registry.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object to be removed.</param>
    public void Delete(IAnimal animal);
    
    /// <summary>
    /// Deletes all pets of a client from the clinic registry.
    /// </summary>
    /// <param name="client"><see cref="IClient"/> object whose pets are to be deleted.</param>
    public void Delete(IClient client);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animal"></param>
    /// <param name="timeSpan"></param>
    /// <returns></returns>
    public IReadOnlyDictionary<DateTime, DateTime?> VisitHistory(IAnimal animal, TimeSpan? timeSpan);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="days"></param>
    /// <returns></returns>
    public List<IAnimal> GetUpcoming(int days = 7);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animal"></param>
    /// <param name="date"></param>
    public void RecordVisit(IAnimal animal, DateTime date);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="animal"></param>
    /// <param name="date"></param>
    public void RecordDischarge(IAnimal animal, DateTime date);

    public void ScheduleProcedure(IAnimal animal, DateTime date, string name);
    public void PerformProcedure(IAnimal animal, string name);
}