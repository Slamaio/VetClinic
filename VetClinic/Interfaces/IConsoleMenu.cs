namespace VetClinic.Interfaces;

public interface IConsoleMenu : IMenu
{
    /// <summary>
    /// A string to separate the number and the description of the options. 
    /// </summary>
    public string Separator { get; set; }
    
    /// <summary>
    /// Whether to show the option numbers or not.
    /// </summary>
    public bool ShowNumber { get; set; }
    
    /// <summary>
    /// Whether the menu has an active exit option.
    /// </summary>
    public bool SoftExit { get; set; }
    
    /// <summary>
    /// Prompt the user to choose an option.
    /// </summary>
    /// <returns>Index of the chosen option.</returns>
    public int GetInput();
    
    /// <summary>
    /// A menu to return to.
    /// </summary>
    public new IConsoleMenu? ParentMenu { get; }
    
    /// <summary>
    /// Start a menu loop.
    /// </summary>
    public void Menu();
}