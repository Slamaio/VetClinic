namespace VetClinic;

public struct Procedure
{
    public string Name;
    public DateTime PlannedDate;
    public DateTime? PerformedDate;
    public IClinic Clinic;

    public bool Performed => PerformedDate != null;
}