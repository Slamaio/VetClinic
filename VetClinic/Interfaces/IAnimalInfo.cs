namespace VetClinic.Interfaces;

public interface IAnimalInfo
{
    public IAnimal Animal { get; }
    public Dictionary<DateTime, DateTime?> VisitRecord { get; }
    
    /// <summary>
    /// Record when an animal visited or was discharged from a clinic.
    /// </summary>
    /// <param name="date">A date of the visit/discharge.</param>
    public void AddRecord(DateTime date);
}