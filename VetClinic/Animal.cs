namespace VetClinic;

public class Animal : IAnimal
{
    private IClient? _owner;
    private List<Procedure> _procedures = new();

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

    public List<Procedure> PerformedProcedures =>
        _procedures.Where(p => p.PerformedDate != null).ToList();

    public List<Procedure> ScheduledProcedures =>
        _procedures.Where(p => p.PerformedDate == null).ToList();

    public bool HasOwner() => Owner != null;
}