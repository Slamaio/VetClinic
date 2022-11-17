namespace VetClinic;

public interface IClient
{
    public string Name { get; set; }
    public List<IAnimal> Pets { get; }
}