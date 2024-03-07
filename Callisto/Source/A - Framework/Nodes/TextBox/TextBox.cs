using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Nodex;

class TextBox : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public Vector2f Size = new(300, 25);
    public Vector2f Origin = new(0, 0);

    public string Text = "";
    public int MaxCharacters = int.MaxValue;

    public List<char> AllowedCharacters = [];

    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.Fonts["RobotoMono"];

    public TextBoxStyle Style = new();

    private bool isSelected = false;
    private int caretX = 0;
    private float padding = 8;
    private const int BackspaceKey = 8;

    private int caretTimer = 0;
    private const int CaretMinTime = 0;
    private int caretMaxTime = 2000;

    private byte caretAlpha = 255;
    private const int CaretMinAlpha = 0;
    private const int CaretMaxAlpha = 255;

    private RectangleShape rectangleRenderer = new();
    private Text textRenderer = new();
    private Text caretRenderer = new();

    #endregion

    // Public

    public override void Start()
    {
        ConnectToEvents();
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
        DisconnectFromEvents();
    }

    public override void Activate()
    {
        base.Activate();

        ConnectToEvents();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        DisconnectFromEvents();
    }

    // Drawing

    private void DrawShape()
    {
        rectangleRenderer.Position = GlobalPosition;
        rectangleRenderer.Size = Size;
        rectangleRenderer.Origin = Origin;
        rectangleRenderer.OutlineThickness = Style.OutlineThickness;
        rectangleRenderer.OutlineColor = isSelected ? Style.SelectedOutlineColor : Style.DeselectedOutlineColor;
        rectangleRenderer.FillColor = Style.FillColor;

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

        float characterWidth = textRenderer.GetLocalBounds().Width / Text.Length;
        float x = GlobalPosition.X + padding + characterWidth * caretX;
        float y = GlobalPosition.Y;

        caretRenderer.Position = new(x, y);
        caretRenderer.FillColor = new(255, 255, 255, caretAlpha);

        if (caretTimer > caretMaxTime)
        {
            caretAlpha = (byte)(caretAlpha == CaretMaxAlpha ? CaretMinAlpha : CaretMaxAlpha);
            caretTimer = CaretMinTime;
        }

        caretTimer ++;

        Window.Draw(caretRenderer);
    }

    // Operations

    private void InsertCharacter(char character)
    {
        if (Text.Length >= MaxCharacters) return;

        if (AllowedCharacters.Count > 0)
        {
            if (!AllowedCharacters.Contains(character))
            {
                return;
            }
        }

        Text = Text.Insert(caretX, character.ToString());
        caretX ++;
        caretAlpha = CaretMaxAlpha;
    }

    private void DeleteLastCharacter()
    {
        if (Text.Length > 0)
        {
            Text = Text[..^1];
            caretX--;
        }
    }

    // Private

    private bool IsMouseOver(Vector2f mousePosition)
    {
        mousePosition = Window.MapPixelToCoords((Vector2i)mousePosition);

        bool matchX1 = mousePosition.X > GlobalPosition.X;
        bool matchX2 = mousePosition.X < GlobalPosition.X + Size.X;

        bool matchY1 = mousePosition.Y > GlobalPosition.Y;
        bool matchY2 = mousePosition.Y < GlobalPosition.Y + Size.Y;

        return matchX1 && matchX2 && matchY1 && matchY2;
    }

    // Event connection

    private void ConnectToEvents()
    {
        Window.KeyPressed += OnKeyPressed;
        Window.TextEntered += OnTextEntered;
        Window.MouseButtonPressed += OnMouseClicked;
    }

    private void DisconnectFromEvents()
    {
        Window.KeyPressed -= OnKeyPressed;
        Window.TextEntered -= OnTextEntered;
        Window.MouseButtonPressed -= OnMouseClicked;
    }

    // Events

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            isSelected = true;
            caretX = Text.Length;
        }
        else
        {
            isSelected = false;
        }
    }

    private void OnTextEntered(object? sender, TextEventArgs e)
    {
        if (!isSelected) return;

        char unicode = e.Unicode[0];

        switch (unicode)
        {
            default:
                InsertCharacter(unicode);
                break;

            case (char)BackspaceKey:
                DeleteLastCharacter();
                break;
        }
    }

    private void OnKeyPressed(object? sender, KeyEventArgs e)
    {
        switch (e.Code)
        {
            case Keyboard.Key.Right:
                if (caretX < Text.Length)
                {
                    caretX ++;
                }
                break;

            case Keyboard.Key.Left:
                if (caretX > 0)
                {
                    caretX --;
                }
                break;

            case Keyboard.Key.Enter:
                isSelected = false;
                break;
        }
    }
}