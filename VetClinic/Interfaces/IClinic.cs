using VetClinic.Modules;
using TimeSpan = VetClinic.Modules.TimeSpan;

namespace VetClinic.Interfaces;

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
    public void AddAnimal(IAnimal animal);

    /// <summary>
    /// Deletes an animal from the clinic registry.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object to be removed.</param>
    public void DeleteAnimal(IAnimal animal);
    
    /// <summary>
    /// Deletes all pets of a client from the clinic registry.
    /// </summary>
    /// <param name="client"><see cref="IClient"/> object whose pets are to be deleted.</param>
    public void DeleteClient(IClient client);

    /// <summary>
    /// Returns a collection of visit-discharge date pairs.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object to get history form.</param>
    /// <param name="timeSpan">A time span over which to show the history.</param>
    /// <returns>A collection of visit-discharge date pairs.</returns>
    public IReadOnlyDictionary<DateTime, DateTime?> VisitHistory(IAnimal animal, TimeSpan timeSpan);

    /// <summary>
    /// Get the list of animals which have a medical procedure scheduled in a given time span.
    /// </summary>
    /// <param name="days">Number of days to define the time span.</param>
    /// <returns>A list of <see cref="IAnimal"/> objects.</returns>
    public List<IAnimal> GetUpcoming(int days = 7);
    
    /// <summary>
    /// Record when an animal visited the clinic.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object.</param>
    /// <param name="date">A date of the visit.</param>
    public void AddVisit(IAnimal animal, DateTime date);

    /// <summary>
    /// Record when an animal was discharged from the clinic.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> object.</param>
    /// <param name="date">A date of the discharge.</param>
    public void AddDischarge(IAnimal animal, DateTime date);

    /// <summary>
    /// Schedule a medical procedure.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> for whom a procedure is scheduled.</param>
    /// <param name="name">Name of the procedure.</param>
    /// <param name="date">Expected date.</param>
    public void ScheduleProcedure(IAnimal animal, string name, DateTime date);
    
    /// <summary>
    /// Mark a medical procedure as performed.
    /// </summary>
    /// <param name="animal"><see cref="IAnimal"/> on whom a procedure was performed.</param>
    /// <param name="name">Name of the procedure.</param>
    /// <param name="date">When the procedure was performed.</param>
    public void PerformProcedure(IAnimal animal, string name, DateTime date);
}