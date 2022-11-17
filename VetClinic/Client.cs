namespace VetClinic;

public class Client : IClient
{
    public Client(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
    public List<IAnimal> Pets { get; } = new();
}