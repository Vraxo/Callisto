using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Callisto;

class TextBox : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    public Vector2f Size = new(300, 25);
    public Vector2f Origin = new(0, 0);
    public string Text = "";
    public int MaxCharacters = int.MaxValue;
    public int MinCharacters = 0;
    public List<char> AllowedCharacters = [];
    public TextBoxStyle Style = new();
    public Text TextRenderer = new();
    public bool Selected = false;

    private const int BackspaceKey = 8;
    private bool IsControlPressed = false;
    private Caret caret;
    private RectangleShape rectangleRenderer = new();

    #endregion

    // Public

    public override void Start()
    {
        caret = new();

        AddChild(caret);

        ConnectToEvents();
    }

    public override void Update()
    {
        DrawShape();
        DrawText();

        base.Update();
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
        rectangleRenderer.OutlineColor = Selected ? Style.SelectedOutlineColor : Style.DeselectedOutlineColor;
        rectangleRenderer.FillColor = Style.FillColor;

        Window.Draw(rectangleRenderer);
    }

    private void DrawText()
    {
        TextRenderer.DisplayedString = Text;
        TextRenderer.CharacterSize = Style.FontSize;
        TextRenderer.Color = Style.TextColor;
        TextRenderer.Font = Style.Font;

        float x = GlobalPosition.X + Style.Padding - Origin.X;
        //float y = GlobalPosition.Y + rectangleRenderer.GetLocalBounds().Height / 2 - TextRenderer.GetLocalBounds().Height;
        float y = GlobalPosition.Y + Size.Y / 2 - TextRenderer.GetLocalBounds().Height / 1.5F;

        TextRenderer.Position = new(x, y);

        Window.Draw(TextRenderer);
    }

    // Operations

    private void InsertCharacter(char character)
    {
        if (IsControlPressed) return;

        if (Text.Length >= MaxCharacters) return;

        if (AllowedCharacters.Count > 0)
        {
            if (!AllowedCharacters.Contains(character))
            {
                return;
            }
        }

        Text = Text.Insert(caret.X, character.ToString());
        caret.X ++;
    }

    private void DeleteLastCharacter()
    {
        if (caret.X > 0 && Text.Length > MinCharacters)
        {
            Text = Text.Remove(caret.X - 1, 1);
            caret.X --;
        }
    }

    private void PasteText()
    {
        if (IsControlPressed)
        {
            IsControlPressed = false;

            foreach (char character in Clipboard.Contents.ToCharArray())
            {
                InsertCharacter(character);
            }

            IsControlPressed = true;
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
        Window.KeyReleased += OnKeyReleased;
        Window.TextEntered += OnTextEntered;
        Window.MouseButtonPressed += OnMouseClicked;
    }

    private void DisconnectFromEvents()
    {
        Window.KeyPressed -= OnKeyPressed;
        Window.KeyReleased -= OnKeyReleased;
        Window.TextEntered -= OnTextEntered;
        Window.MouseButtonPressed -= OnMouseClicked;
    }

    // Events

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button != Mouse.Button.Left) return;

        if (IsMouseOver(new(e.X, e.Y)))
        {
            Selected = true;
            caret.GetInPosition(e.X);
        }
        else
        {
            Selected = false;
        }
    }

    private void OnTextEntered(object? sender, TextEventArgs e)
    {
        if (!Selected) return;

        char unicode = e.Unicode[0];

        switch (unicode)
        {
            default:
                InsertCharacter(unicode);
                break;

            case (char)BackspaceKey:
                DeleteLastCharacter();
                break;

            case (char)22:
                break;
        }
    }

    private void OnKeyPressed(object? sender, KeyEventArgs e)
    {
        if (!Selected) return;

        switch (e.Code)
        {
            case Keyboard.Key.Right:
                caret.X ++;
                break;

            case Keyboard.Key.Left:
                caret.X --;
                break;

            case Keyboard.Key.Enter:
                Selected = false;
                break;

            case Keyboard.Key.LControl:
            case Keyboard.Key.RControl:
                IsControlPressed = true;
                break;

            case Keyboard.Key.V:
                PasteText();
                break;
        }
    }

    private void OnKeyReleased(object? sender, KeyEventArgs e)
    {
        switch (e.Code)
        {
            case Keyboard.Key.LControl:
            case Keyboard.Key.RControl:
                IsControlPressed = false;
                break;
        }
    }
}