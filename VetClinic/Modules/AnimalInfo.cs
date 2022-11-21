using VetClinic.Interfaces;

namespace VetClinic.Modules;

[Serializable]
public record AnimalInfo(IAnimal Animal) : IAnimalInfo
{
    private DateTime? _lastVisit;

    public IAnimal Animal { get; } = Animal;
    public Dictionary<DateTime, DateTime?> VisitRecord { get; } = new();

    public void AddRecord(DateTime date)
    {
        if (date < _lastVisit)
            throw new ArgumentException("Can't travel back in time.");
        
        if (_lastVisit == null || VisitRecord[(DateTime)_lastVisit] != null)
        {
            VisitRecord[date] = null;
            _lastVisit = date;
        }
        else
            VisitRecord[(DateTime)_lastVisit] = date;
    }
}