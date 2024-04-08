using SFML.Window;
using Callisto.ContactsListNode;

namespace Callisto;

class EntryPoint
{
    [STAThread]
    public static void Main(string[] args)
    {
        WindowInfo windowInfo = new()
        {
            VideoMode = new(360, 640),
            Title = "Callisto",
            Styles = Styles.Default,
            ContextSettings = new(0, 0, 16),
            ClearColor = new(32, 32, 32)
        };

        Program.Start(windowInfo, new ContactsList());
    }
}