namespace VetClinic.Interfaces;

public interface IAnimal
{
    public string Name { get; set; }
    public string Type { get; }
    public IClient? Owner { get; set; }
    
    public IReadOnlyDictionary<string, DateTime> PerformedProcedures { get; }
    public IReadOnlyDictionary<string, DateTime> ScheduledProcedures { get; }

    /// <summary>
    /// Schedule a medical procedure.
    /// </summary>
    /// <param name="name">Name of the procedure.</param>
    /// <param name="date">Expected date.</param>
    public void ScheduleProcedure(string name, DateTime date);
    
    /// <summary>
    /// Mark a medical procedure as performed.
    /// </summary>
    /// <param name="name">Name of the procedure</param>
    /// <param name="date">When the procedure was performed.</param>
    public void PerformProcedure(string name, DateTime date);
    
    /// <summary>
    /// Check if an animal has an owner.
    /// </summary>
    public bool HasOwner();
}