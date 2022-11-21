using VetClinic.Interfaces;

namespace VetClinic.Modules;

[Serializable]
public class Client : IClient
{
    public Client(string name)
    {
        Name = name;
    }
    
    public string Name { get; set; }
    public List<IAnimal> Pets { get; } = new();
}