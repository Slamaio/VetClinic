namespace VetClinic;

public interface IAnimal
{
    public string Name { get; set; }
    public string Type { get; }
    public IClient? Owner { get; set; }
    public List<Procedure> PerformedProcedures { get; }
    public List<Procedure> ScheduledProcedures { get; }

    // TODO: schedule and perform
    
    public bool HasOwner();
}