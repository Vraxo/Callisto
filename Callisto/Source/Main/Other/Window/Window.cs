using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Callisto;

class Window : RenderWindow
{
    // Fields

    public Node RootNode;
    public Color ClearColor;
    public float ViewY = 0;

    private Vector2u originalSize;
    private bool maintainOriginalSize = true;

    // Constructor

    public Window(WindowInfo info) : 
    base(info.VideoMode, info.Title, info.Styles, info.ContextSettings)
    {
        ClearColor = info.ClearColor;
        originalSize = new(info.VideoMode.Width, info.VideoMode.Height);

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
        ViewY = 0;
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
        if (maintainOriginalSize)
        {
            float x = Size.X < originalSize.X ? originalSize.X : Size.X;
            float y = Size.Y < originalSize.Y ? originalSize.Y : Size.Y;
            
            Size = new((uint)x, (uint)y);
        }

        FloatRect visibleArea = new(0, ViewY, Size.X, Size.Y);
        SetView(new(visibleArea));
    }
}