using VetClinic.Interfaces;

namespace VetClinic.Modules;

[Serializable]
public class Animal : IAnimal
{
    private IClient? _owner;
    private readonly Dictionary<string, DateTime> _procedures = new();

    public Animal(string name, string type, IClient? owner = null)
    {
        Name = name;
        Type = type;
        Owner = owner;
    }

    public string Name { get; set; }
    public string Type { get; }

    public IClient? Owner
    {
        get => _owner;
        set
        {
            _owner?.Pets.Remove(this);
            _owner = value;
            _owner?.Pets.Add(this);
        }
    }

    public IReadOnlyDictionary<string, DateTime> PerformedProcedures =>
        _procedures
            .Where(p => p.Value < DateTime.Now)
            .ToDictionary(p => p.Key, p => p.Value);

    public IReadOnlyDictionary<string, DateTime> ScheduledProcedures =>
        _procedures
            .Where(p => p.Value >= DateTime.Now)
            .ToDictionary(p => p.Key, p => p.Value);

    public void ScheduleProcedure(string name, DateTime date)
    {
        _procedures[name] = date;
    }

    public void PerformProcedure(string name, DateTime date)
    {
        _procedures[name] = date;
    }

    public bool HasOwner() => Owner != null;
}