using SFML.Window;
using SFML.Graphics;
using View = SFML.Graphics.View;

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
        // Ensure a minimum window size
        float minWidth = 360;
        float minHeight = 640;
        float newX = Math.Max(e.Width, minWidth);
        float newY = Math.Max(e.Height, minHeight);

        // Update the window size
        Size = new((uint)newX, (uint)newY);

        // Recalculate view center based on new window size
        float viewCenterX = newX / 2f;
        float viewCenterY = newY / 2f;

        // Adjust the view
        View view = new(new FloatRect(0, 0, newX, newY));
        view.Center = new(viewCenterX, viewCenterY);
        SetView(view);
    }
}