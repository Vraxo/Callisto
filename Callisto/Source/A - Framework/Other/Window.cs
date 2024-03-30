using SFML.Window;
using SFML.System;
using SFML.Graphics;
using System.Windows.Forms;

namespace Nodex;

class Window : RenderWindow
{
    // Fields

    public Node RootNode;
    public Color ClearColor = new(32, 32, 32);

    public float ViewY = 0;

    // Constructor

    public Window(WindowInfo info) : base(info.VideoMode, info.Title, info.Styles, info.ContextSettings)
    {
        prefviousSize = new((int)info.VideoMode.Width, (int)info.VideoMode.Height);
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
        float x = Size.X < 360 ? 360 : Size.X;
        float y = Size.Y < 640 ? 640 : Size.Y;

        Size = new((uint)x, (uint)y);

        FloatRect visibleArea = new(0, ViewY, Size.X, Size.Y);
        SetView(new(visibleArea));
    }
}