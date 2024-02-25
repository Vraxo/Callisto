using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Nodex;

class CircleButton : Node
{
    // Fields

    public float    Radius;
    public Vector2f Origin;
    public bool     IsVisible;

    protected Action actionOnClick;

    private bool isSelected = false;

    // Public

    public override void Start()
    {
        base.Start();

        Window.MouseButtonPressed  += OnMouseClicked;
        Window.MouseButtonReleased += OnMouseReleased;
    }

    public override void Update()
    {
        base.Update();

        DrawDebugCircle();
    }

    public override void Destroy()
    {
        base.Destroy();

        Window.MouseButtonPressed  -= OnMouseClicked;
        Window.MouseButtonReleased -= OnMouseClicked;
    }

    // Private

    private void DrawDebugCircle()
    {
        if (IsVisible)
        {
            CircleShape c = new()
            {
                Position         = Position,
                Radius           = Radius,
                Origin           = Origin,
                OutlineColor     = Color.Blue,
                FillColor        = Color.Transparent,
                OutlineThickness = 1,
            };

            Window.Draw(c);
        }
    }

    private bool IsInRange(Vector2f position)
    {
        bool isInRange = GetDistance(GlobalPosition, position) < Radius;
        return isInRange;
    }

    private float GetDistance(Vector2f point1, Vector2f point2)
    {
        float xDistance = MathF.Pow(point2.X - point1.X, 2);
        float yDistance = MathF.Pow(point2.Y - point1.Y, 2);

        float distance = MathF.Sqrt(xDistance + yDistance);

        return distance;
    }

    // Events

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (IsInRange(new(e.X, e.Y)))
        {
            isSelected = true;
        }
    }

    private void OnMouseReleased(object? sender, MouseButtonEventArgs e)
    {
        if (IsInRange(new(e.X, e.Y)))
        {
            if (isSelected)
            {
                actionOnClick();
            }
        }

        isSelected = false;
    }
}