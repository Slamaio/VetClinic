namespace VetClinic;

public interface IAnimal
{
    public string Name { get; set; }
    public string Type { get; }
    public IClient? Owner { get; set; }
    public IReadOnlyDictionary<string, DateTime> PerformedProcedures { get; }
    public IReadOnlyDictionary<string, DateTime> ScheduledProcedures { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="date"></param>
    public void ScheduleProcedure(string name, DateTime date);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void PerformProcedure(string name);
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public bool HasOwner();
}