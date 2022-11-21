namespace VetClinic.Interfaces;

public interface IMenu : IList<IMenuItem>
{
    public string? Title { get; set; }
    
    /// <summary>
    /// A menu to return to.
    /// </summary>
    public IMenu ParentMenu { get; }
    
    /// <summary>
    /// Display all menu items.
    /// </summary>
    public void Show();
    
    /// <summary>
    /// Execute a job assigned to the given option.
    /// </summary>
    /// <param name="opt">Index of the option to be executed.</param>
    public void Activate(int opt);
}