using Callisto.ContactsListNode;
using SFML.Window;

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
        WindowInfo windowInfo = new()
        {
            VideoMode = new(360, 640),
            Title = "Callisto",
            Styles = Styles.Default,
            ContextSettings = new(0, 0, 16)
        };

        MainWindow = new(windowInfo);
        Windows.Add(MainWindow);
        MainWindow.RootNode = new ContactsList();
        MainWindow.Start();
    }
}