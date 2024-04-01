using SFML.System;
using SFML.Window;

namespace Nodex;

class Program
{
    // AllFields

    public static Window MainWindow { get; set; }
    public static List<Window> Windows = [];

    // Public

    [STAThread]
    public static void Start(Node rootNode)
    {
        CreateMainWindow(rootNode);

        Clock deltaTimeClock = new();

        while (true)
        {
            for (int i = 0; i < Windows.Count; i++)
            {
                Windows[i].Update();
            }

            DeltaTimer.DeltaTime = deltaTimeClock.Restart();
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

    private static void CreateMainWindow(Node rootNode)
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
        MainWindow.RootNode = rootNode;
        MainWindow.Start();
    }
}