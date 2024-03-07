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

    // Private

    private void AdjustView()
    {
        View oldView = GetView();
        
        float y = MathF.Abs((Size.Y / 2) - oldView.Center.Y);

        float remainder = y % 50;

        if (remainder < 25)
        {
            y -= remainder;
        }
        else
        {
            y += 50 - remainder;
        }

        FloatRect visibleArea = new(0, y, Size.X, Size.Y);
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

        AdjustView();
    }
}