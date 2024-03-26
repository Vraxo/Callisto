using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nodex;

class Button : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public Vector2f Size = new(100, 25);
    public Vector2f Origin = new(0, 0);
    public Vector2f TextOrigin = new(0, 0);

    public string Text = "";
    public ButtonStyle Style = new();

    public Action OnClick = () => { };
    public Action<Button> OnUpdate = (button) => { };

    private bool pressed = false;

    private Text textRenderer = new();
    private RectangleShape rectangleRenderer = new();

    #endregion

    // Public

    public override void Start()
    {
        ConnectToEvents();
    }

    public override void Update()
    {
        base.Update();
        OnUpdate(this);
        DrawShape();
        DrawText();
    }

    public override void Destroy()
    {
        DisconnectFromEvents();
    }

    public override void Activate()
    {
        base.Activate();

        Start();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        Destroy();
    }

    // Private

    private void DrawShape()
    {
        rectangleRenderer.Position = GlobalPosition;
        rectangleRenderer.Size = Size;
        rectangleRenderer.Origin = Origin;
        rectangleRenderer.OutlineThickness = Style.OutlineThickness;
        rectangleRenderer.OutlineColor = Style.OutlineColor;
        rectangleRenderer.FillColor = Style.FillColor;

        Window.Draw(rectangleRenderer);
    }

    private void DrawText()
    {
        textRenderer.DisplayedString = Text;
        textRenderer.FillColor = Style.TextColor;
        textRenderer.Position = GlobalPosition;
        textRenderer.Font = Style.Font;
        textRenderer.CharacterSize = Style.FontSize;

        float x = GlobalPosition.X - Origin.X + Size.X / 2 - textRenderer.GetLocalBounds().Width / 2;
        float y = GlobalPosition.Y + rectangleRenderer.GetLocalBounds().Height / 2 - textRenderer.GetLocalBounds().Height - Origin.Y;

        textRenderer.Position = new(x, y);

        Window.Draw(textRenderer);
    }

    private bool IsMouseOver(Vector2f mousePosition)
    {
        mousePosition = Window.MapPixelToCoords((Vector2i)mousePosition);

        bool matchX1 = mousePosition.X > GlobalPosition.X - Origin.X;
        bool matchX2 = mousePosition.X < GlobalPosition.X + Size.X - Origin.X;

        bool matchY1 = mousePosition.Y > GlobalPosition.Y - Origin.Y;
        bool matchY2 = mousePosition.Y < GlobalPosition.Y + Size.Y - Origin.Y;

        return matchX1 && matchX2 && matchY1 && matchY2;
    }

    // Connection to events

    private void ConnectToEvents()
    {
        Window.MouseMoved += OnMouseMoved;
        Window.MouseButtonPressed += OnMouseClicked;
        Window.MouseButtonReleased += OnMouseReleased;
    }

    private void DisconnectFromEvents()
    {
        Window.MouseMoved -= OnMouseMoved;
        Window.MouseButtonPressed -= OnMouseClicked;
        Window.MouseButtonReleased -= OnMouseReleased;
    }

    // Events

    private void OnMouseMoved(object? sender, MouseMoveEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            Style.FillColor = pressed ? Style.ActiveFillColor : Style.HoverFillColor;
        }
        else
        {
            Style.FillColor = Style.IdleFillColor;
        }
    }

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button != Mouse.Button.Left) return;

        if (IsMouseOver(new(e.X, e.Y)))
        {
            pressed = true;
            Style.FillColor = Style.ActiveFillColor;
        }
    }

    private void OnMouseReleased(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button != Mouse.Button.Left) return;

        if (IsMouseOver(new(e.X, e.Y)))
        {
            if (pressed)
            {
                OnClick();
            }
        }

        pressed = false;
        Style.FillColor = Style.IdleFillColor;
    }
}