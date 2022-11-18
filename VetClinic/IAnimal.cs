namespace VetClinic;

public interface IAnimal
{
    public string Name { get; set; }
    public string Type { get; }
    public IClient? Owner { get; set; }
    public IReadOnlyDictionary<string, DateTime> PerformedProcedures { get; }
    public IReadOnlyDictionary<string, DateTime> ScheduledProcedures { get; }

    public void ScheduleProcedure(string name, DateTime date);
    public void PerformProcedure(string name);
    
    public bool HasOwner();
}