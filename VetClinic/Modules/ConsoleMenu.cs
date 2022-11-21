using VetClinic.Interfaces;

namespace VetClinic.Modules;

public class ConsoleMenu : List<IMenuItem>, IConsoleMenu
{
    public ConsoleMenu(string title, string separator, IConsoleMenu? parentMenu = null, bool softExit = false,
        bool showNumber = true)
    {
        Title = title;
        Separator = separator;
        ParentMenu = parentMenu ?? this;
        ShowNumber = showNumber;
        SoftExit = softExit;
        Add(ConsoleMenuItem.Exit);
    }

    public ConsoleMenu(IEnumerable<ConsoleMenuItem> collection, string title, string separator,
        IConsoleMenu? parentMenu = null, bool softExit = true,
        bool showNumber = true) : base(collection)
    {
        Title = title;
        Separator = separator;
        ParentMenu = parentMenu ?? this;
        ShowNumber = showNumber;
        SoftExit = softExit;
        Add(ConsoleMenuItem.Exit);
    }

    public ConsoleMenu(int capacity, string title, string separator, IConsoleMenu? parentMenu = null,
        bool softExit = true, bool showNumber = true) :
        base(capacity)
    {
        Title = title;
        Separator = separator;
        ParentMenu = parentMenu ?? this;
        ShowNumber = showNumber;
        SoftExit = softExit;
        Add(ConsoleMenuItem.Exit);
    }

    public string? Title { get; set; }
    public string Separator { get; set; }
    public bool ShowNumber { get; set; }
    public bool SoftExit { get; set; }

    public IConsoleMenu ParentMenu { get; }

    IMenu IMenu.ParentMenu => ParentMenu;

    public void Show()
    {
        Console.Clear();
        Console.WriteLine(Title);
        Console.WriteLine(new string('-', Title?.Length ?? 0));
        for (int i = 1; i < Count; i++)
            Console.WriteLine($"{(ShowNumber ? $"  {i}{Separator}" : "")}{this[i].Description}");
        Console.WriteLine($"{(ShowNumber ? $"  {0}{Separator}" : "")}{this[0].Description}");
    }

    public void Activate(int opt)
    {
        if (SoftExit && opt == 0)
        {
            ParentMenu.Menu();
            return;
        }
        
        if (opt <= Count - 1)
            this[opt].Execute();
        
        Console.Write("\nPress any key to go to the menu...");
        Console.ReadKey();
        Menu();
    }

    public int GetInput()
    {
        int cursorTop = Console.CursorTop + 1;
        int userInput;
        do
        {
            // Re-display the prompt
            Console.SetCursorPosition(0, cursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, cursorTop);

            Console.Write($"Enter a choice (0 - {Count - 1}): ");
        } while (!int.TryParse(Console.ReadLine(), out userInput)
                 || userInput < 0
                 || userInput > Count - 1);

        return userInput;
    }

    public void Menu()
    {
        Show();
        var opt = GetInput();
        Activate(opt);
    }
}