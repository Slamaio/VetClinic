namespace VetClinic;

public interface IAnimalInfo
{
    public IAnimal Animal { get; }
    public Dictionary<DateTime, DateTime?> VisitRecord { get; }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    public void AddVisit(DateTime date);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="date"></param>
    public void AddDischarge(DateTime date);
}