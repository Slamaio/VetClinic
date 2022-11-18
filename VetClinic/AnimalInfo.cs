namespace VetClinic;

public record AnimalInfo(IAnimal Animal) : IAnimalInfo
{
    private DateTime? _lastVisit;

    public IAnimal Animal { get; } = Animal;
    public Dictionary<DateTime, DateTime?> VisitRecord { get; } = new();

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