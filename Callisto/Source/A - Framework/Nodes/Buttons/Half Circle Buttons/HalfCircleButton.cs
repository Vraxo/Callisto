using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Nodex;

class HalfCircleButton : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public float Radius = 100;
    public Vector2f Origin = new(100, 100);
    public Vector2f TextOrigin = new(0, 0);

    public string Text = "";
    public ButtonStyle Style = new();

    public Action OnClick = () => { };
    public Action<HalfCircleButton> OnUpdate = (button) => { };

    public bool Visible = true;

    protected float renderTextureY = 0;
    protected bool pressed = false;
    protected Text textRenderer = new();

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

        if (!Visible) return;

        DrawShape(renderTextureY);
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

    protected void DrawShape(float y)
    {
        RenderTexture renderTexture = new((uint)(Radius * 2), (uint)Radius);

        renderTexture.Draw(new CircleShape
        {
            Position = new(0, y),
            Radius = Radius,
            FillColor = Style.FillColor,
        });

        renderTexture.Display();

        Window.Draw(new SFML.Graphics.Sprite(renderTexture.Texture)
        {
            Position = Position,
            Origin = Origin
        });
    }

    private void DrawText()
    {
        textRenderer.DisplayedString = Text;
        textRenderer.FillColor = Style.TextColor;
        textRenderer.Position = GlobalPosition;
        textRenderer.Font = Style.Font;
        textRenderer.CharacterSize = Style.FontSize;

        float x = GlobalPosition.X - Origin.X + Radius - textRenderer.GetLocalBounds().Width / 2;
        float y = GlobalPosition.Y + Radius / 2 - textRenderer.GetLocalBounds().Height - Origin.Y;

        textRenderer.Position = new(x, y);

        Window.Draw(textRenderer);
    }

    protected virtual bool IsMouseOver(Vector2f mousePosition)
    {
        if (mousePosition.Y > GlobalPosition.Y) return false;

        return IsInRange(mousePosition);
    }

    protected bool IsInRange(Vector2f position)
    {
        bool isInRange = GetDistance(GlobalPosition, position) < Radius;
        return isInRange;
    }

    private static float GetDistance(Vector2f point1, Vector2f point2)
    {
        float xDistance = MathF.Pow(point2.X - point1.X, 2);
        float yDistance = MathF.Pow(point2.Y - point1.Y, 2);

        float distance = MathF.Sqrt(xDistance + yDistance);

        return distance;
    }

    // Connection to events

    protected void ConnectToEvents()
    {
        Window.MouseMoved += OnMouseMoved;
        Window.MouseButtonPressed += OnMouseClicked;
        Window.MouseButtonReleased += OnMouseReleased;
    }

    protected void DisconnectFromEvents()
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
            Style.FillColor = pressed ? Style.PressedFillColor : Style.HoverFillColor;
        }
        else
        {
            Style.FillColor = Style.UnpressedFillColor;
        }
    }

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button != Mouse.Button.Left) return;

        if (IsMouseOver(new(e.X, e.Y)))
        {
            pressed = true;
            Style.FillColor = Style.PressedFillColor;
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
        Style.FillColor = Style.UnpressedFillColor;
    }
}