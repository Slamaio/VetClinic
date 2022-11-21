using VetClinic.Interfaces;

namespace VetClinic.Modules;

public class ConsoleMenuItem : IMenuItem
{
    public static ConsoleMenuItem Exit => new("Exit", () => Environment.Exit(0));


    public ConsoleMenuItem(string description, Action execute)
    {
        Description = description;
        Execute = execute;
    }

    public string Description { get; set; }
    public Action Execute { get; set; }
}