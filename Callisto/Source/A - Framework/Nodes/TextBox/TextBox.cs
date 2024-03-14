using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Net.Http.Headers;

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
    public TextBoxStyle Style = new();
    public Text TextRenderer = new();
    public bool IsSelected = false;

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
        rectangleRenderer.OutlineColor = IsSelected ? Style.SelectedOutlineColor : Style.DeselectedOutlineColor;
        rectangleRenderer.FillColor = Style.FillColor;

        Window.Draw(rectangleRenderer);
    }

    private void DrawText()
    {
        float x = (int)(GlobalPosition.X + Style.Padding - Origin.X);
        float y = (int)(GlobalPosition.Y + Size.Y / 10 - Origin.Y);

        TextRenderer.Position = new(x, y);
        TextRenderer.CharacterSize = Style.FontSize;
        TextRenderer.DisplayedString = Text;
        TextRenderer.Font = Style.Font;

        Window.Draw(TextRenderer);
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

        Text = Text.Insert(caret.X, character.ToString());

        caret.X ++;
    }

    private void DeleteLastCharacter()
    {
        if (Text.Length > 0)
        {
            Text = Text.Remove(caret.X - 1, 1);
            caret.X --;
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
        if (IsMouseOver(new(e.X, e.Y)))
        {
            IsSelected = true;
            caret.X = Text.Length;
        }
        else
        {
            IsSelected = false;
        }
    }

    private void OnTextEntered(object? sender, TextEventArgs e)
    {
        if (!IsSelected) return;

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
        if (!IsSelected) return;

        switch (e.Code)
        {
            case Keyboard.Key.Right:
                if (caret.X < Text.Length)
                {
                    caret.X++;
                }
                break;

            case Keyboard.Key.Left:
                if (caret.X > 0)
                {
                    caret.X--;
                }
                break;

            case Keyboard.Key.Enter:
                IsSelected = false;
                break;

            case Keyboard.Key.LControl:
            case Keyboard.Key.RControl:
                IsControlPressed = true;
                break;

            case Keyboard.Key.V:
                if (IsControlPressed)
                {
                    foreach (char character in Clipboard.Contents.ToCharArray())
                    {
                        InsertCharacter(character);
                    }
                }
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