using SFML.Window;
using Callisto.ContactsListNode;

namespace Nodex;

class Program
{
    // Fields

    public static Window MainWindow { get; set; }
    public static List<Window> Windows = [];

    // Public

    [STAThread]
    public static void Main()
    {
        CreateMainWindow();

        while (true)
        {
            for (int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Update();
            }
        }
    }

    public static void AddWindow(Window window)
    {
        Windows.Add(window);
    }

    public static void RemoveWindow(Window window)
    {
        Windows.Remove(window);
        window.Close();
    }

    // Private

    private static void CreateMainWindow()
    {
        MainWindow = new
        (
            new(360, 640),
            "Callisto",
            Styles.Default,
            new ContextSettings(0, 0, 16)
        );

        Windows.Add(MainWindow);
        MainWindow.RootNode = new ContactsList();

        MainWindow.Start();
    }
}