using SFML.System;
using SFML.Window;

namespace Callisto;

class Program
{
    // Fields

    public static Window MainWindow { get; set; }
    public static List<Window> Windows = [];

    // Public

    public static void Start(WindowInfo windowInfo, Node rootNode)
    {
        AddMainWindow(windowInfo, rootNode);

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

    private static void AddMainWindow(WindowInfo windowInfo, Node rootNode)
    {
        MainWindow = new(windowInfo);
        Windows.Add(MainWindow);
        MainWindow.RootNode = rootNode;
        MainWindow.Start();
    }
}