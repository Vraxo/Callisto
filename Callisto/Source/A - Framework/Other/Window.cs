using SFML.Graphics;
using SFML.Window;

namespace Nodex;

class Window : RenderWindow
{
    // Fields

    public Node RootNode;
    public Color ClearColor = new(32, 32, 32);

    // Constructor

    public Window(VideoMode mode, string title, Styles styles, ContextSettings settings) :
           base(mode, title, styles, settings)
    {
        Resized += OnResized;
        Closed += OnClosed;
    }

    // Public

    public void Start()
    {
        RootNode.Window = this;
        RootNode.Start();
    }

    public void Update()
    {
        DispatchEvents();
        Clear(ClearColor);
        RootNode.Update();
        Display();
    }

    public void ResetView()
    {
        FloatRect visibleArea = new(0, 0, Size.X, Size.Y);
        SetView(new(visibleArea));
    }

    // Events

    private void OnClosed(object? sender, EventArgs e)
    {
        RootNode.Destroy();
        Close();

        if (Program.MainWindow == this)
        {
            Environment.Exit(0);
        }
    }

    private void OnResized(object? sender, SizeEventArgs e)
    {
        float x = Size.X < 360 ? 360 : Size.X;
        float y = Size.Y < 640 ? 640 : Size.Y;

        Size = new((uint)x, (uint)y);

        FloatRect visibleArea = new(0, 0, Size.X, Size.Y);
        SetView(new(visibleArea));
    }
}