namespace VetClinic;

public record AnimalInfo(IAnimal Animal)
{
    private DateTime? _lastVisit;

    public readonly IAnimal Animal = Animal;
    public readonly Dictionary<DateTime, DateTime?> VisitRecord = new();

    // TODO: combine into a single add method 
    
    public void AddVisit(DateTime date)
    {
        if (_lastVisit != null && VisitRecord[(DateTime)_lastVisit] == null)
            throw new Exception("Try AddDischarge() instead.");

        VisitRecord[date] = null;
        _lastVisit = date;
    }

    public void AddDischarge(DateTime date)
    {
        if (_lastVisit == null)
            throw new Exception("Try AddVisit() instead.");
        if (_lastVisit >= date)
            throw new Exception("Try AddVisit() instead.");
        if (VisitRecord[(DateTime)_lastVisit] != null)
            throw new Exception("Try AddVisit() instead.");

        VisitRecord[(DateTime)_lastVisit] = date;
    }
}