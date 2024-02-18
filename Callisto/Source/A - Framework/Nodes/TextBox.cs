using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using Callisto;

namespace Nodex;

class TextBox : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public Vector2f Size = new(300, 25);
    public Vector2f Origin = new(0, 0);

    public string Text = "";
    public int MaximumCharacters = int.MaxValue;

    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.RobotoMono;

    public int OutlineThickness = -1;

    public Color SelectedOutlineColor = new(32, 32, 255);
    public Color DeselectedOutlineColor = new(0, 0, 0, 0);
    public Color FillColor = new(16, 16, 16);
    public Color IdleFillColor = new(16, 16, 16);
    public Color HoverFillColor = new(16, 16, 16);
    public Color ActiveFillColor = new(32, 32, 32);

    private bool isSelected = false;
    private int cursor = 0;
    private float padding = 8;
    private const int Backspace = 8;

    #endregion

    // Public

    public override void Start()
    {
        Window.MouseButtonPressed += OnMouseClicked;
        Window.TextEntered += OnTextEntered;
        Window.KeyPressed += OnKeyPressed;
    }

    public override void Update()
    {
        base.Update();
        DrawShape();
        DrawText();
    }

    public override void Destroy()
    {
        Window.MouseButtonPressed -= OnMouseClicked;
    }

    // Private

    private void DrawShape()
    {
        RectangleShape r = new()
        {
            Position = GlobalPosition,
            Size = Size,
            OutlineThickness = OutlineThickness,
            OutlineColor = isSelected ? SelectedOutlineColor : DeselectedOutlineColor,
            FillColor = FillColor,
            Origin = Origin
        };

        r.Draw(Window, RenderStates.Default);
    }

    private void DrawText()
    {
        Text t = new()
        {
            DisplayedString = Text,
            Position = GlobalPosition,
            Font = Font,
            CharacterSize = FontSize
        };

        float x = (int)(GlobalPosition.X + padding - Origin.X);
        float y = (int)(GlobalPosition.Y + Size.Y / 10 - Origin.Y);

        t.Position = new(x, y);

        t.Draw(Window, RenderStates.Default);
    }

    private bool IsMouseOver(Vector2f mousePosition)
    {
        mousePosition = Window.MapPixelToCoords((Vector2i)mousePosition);

        bool matchX1 = mousePosition.X > GlobalPosition.X;
        bool matchX2 = mousePosition.X < GlobalPosition.X + Size.X;

        bool matchY1 = mousePosition.Y > GlobalPosition.Y;
        bool matchY2 = mousePosition.Y < GlobalPosition.Y + Size.Y;

        return matchX1 && matchX2 && matchY1 && matchY2;
    }

    // Events

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            isSelected = true;
            cursor = Text.Length;
        }
        else
        {
            isSelected = false;
        }
    }

    private void OnTextEntered(object? sender, TextEventArgs e)
    {
        if (!isSelected) return;

        switch (e.Unicode[0])
        {
            default:
                if (Text.Length < MaximumCharacters)
                {
                    Text = Text.Insert(cursor, e.Unicode);
                    cursor++;
                }
                break;

            case (char)Backspace:
                if (Text.Length > 0)
                {
                    Text = Text[..^1];
                    cursor--;
                }
                break;
        }
    }

    private void OnKeyPressed(object? sender, KeyEventArgs e)
    {
        switch (e.Code)
        {
            case Keyboard.Key.Right:
                if (cursor < Text.Length)
                {
                    cursor++;
                }
                break;

            case Keyboard.Key.Left:
                if (cursor > 0)
                {
                    cursor--;
                }
                break;

            case Keyboard.Key.Enter:
                isSelected = false;
                break;
        }
    }
}