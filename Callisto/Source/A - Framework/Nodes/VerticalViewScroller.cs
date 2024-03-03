using SFML.Window;

namespace Nodex;

class VerticalViewScroller : Node
{
    // Fields

    public int Factor = 50;
    public bool CanGoUp = false;
    public bool CanGoDown = false;

    // Public

    public override void Start()
    {
        Window.MouseWheelScrolled += OnMouseWheelScrolled;
    }

    public override void Destroy()
    {
        base.Destroy();

        Window.MouseWheelScrolled -= OnMouseWheelScrolled;
    }

    // Events

    private void OnMouseWheelScrolled(object? sender, MouseWheelScrollEventArgs e)
    {
        if (e.Wheel == Mouse.Wheel.VerticalWheel)
        {
            if (e.Delta > 0 && CanGoUp)
            {
                Window.GetView().Move(new(0, -Factor));
            }
            else if (e.Delta < 0 && CanGoDown)
            {
                Window.GetView().Move(new(0, Factor));
            }

            Window.SetView(Window.GetView());
        }
    }
}