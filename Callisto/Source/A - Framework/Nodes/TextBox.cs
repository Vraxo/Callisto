using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Nodex;

class TextBox : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public Vector2f Size = new(300, 25);
    public Vector2f Origin = new(0, 0);

    public string Text = "";
    public int MaximumCharacters = int.MaxValue;

    public List<char> AllowedCharacters = [];

    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.Fonts["RobotoMono"];

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
    private const int BackspaceKey = 8;

    private byte caretAlpha = 255;
    private int caretTimer = 0;
    private int caretMaxTime = 2000;

    private RectangleShape rectangleRenderer = new();
    private Text textRenderer = new();
    private Text caretRenderer = new();

    #endregion

    // Public

    public override void Start()
    {
        Window.KeyPressed += OnKeyPressed;
        Window.TextEntered += OnTextEntered;
        Window.MouseButtonPressed += OnMouseClicked;
    }

    public override void Update()
    {
        base.Update();

        DrawShape();
        DrawText();
        DrawCaret();
    }

    public override void Destroy()
    {
        Window.KeyPressed -= OnKeyPressed;
        Window.TextEntered -= OnTextEntered;
        Window.MouseButtonPressed -= OnMouseClicked;
    }

    // Private

    private void DrawShape()
    {
        rectangleRenderer.Position = GlobalPosition;
        rectangleRenderer.Size = Size;
        rectangleRenderer.Origin = Origin;
        rectangleRenderer.OutlineThickness = OutlineThickness;
        rectangleRenderer.OutlineColor = isSelected ? SelectedOutlineColor : DeselectedOutlineColor;
        rectangleRenderer.FillColor = FillColor;

        Window.Draw(rectangleRenderer);
    }

    private void DrawText()
    {
        float x = (int)(GlobalPosition.X + padding - Origin.X);
        float y = (int)(GlobalPosition.Y + Size.Y / 10 - Origin.Y);

        textRenderer.Position = new(x, y);
        textRenderer.CharacterSize = FontSize;
        textRenderer.DisplayedString = Text;
        textRenderer.Font = Font;

        Window.Draw(textRenderer);
    }

    private void DrawCaret()
    {
        if (!isSelected) return;

        caretRenderer.DisplayedString = "|";
        caretRenderer.Font = Font;
        caretRenderer.CharacterSize = FontSize;
        caretRenderer.Position = new(GlobalPosition.X + (textRenderer.GetLocalBounds().Width / Text.Length) * (cursor + 1), GlobalPosition.Y);
        caretRenderer.FillColor = new(255, 255, 255, caretAlpha);

        if (caretTimer > caretMaxTime)
        {
            caretAlpha = (byte)(caretAlpha == 255 ? 0 : 255);
            caretTimer = 0;
        }

        caretTimer ++;

        Window.Draw(caretRenderer);
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
                    if (AllowedCharacters.Count > 0)
                    {
                        if (!AllowedCharacters.Contains(e.Unicode[0]))
                        {
                            return;
                        }
                    }

                    Text = Text.Insert(cursor, e.Unicode);
                    cursor ++;
                    caretAlpha = 255;
                }
                break;

            case (char)BackspaceKey:
                if (Text.Length > 0)
                {
                    Text = Text[..^1];
                    cursor --;
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
                    cursor ++;
                }
                break;

            case Keyboard.Key.Left:
                if (cursor > 0)
                {
                    cursor --;
                }
                break;

            case Keyboard.Key.Enter:
                isSelected = false;
                break;
        }
    }
}