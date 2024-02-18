using SFML.System;
using SFML.Graphics;

namespace Nodex;

class Label : Node
{
    // Fields

    public string Text = "";
    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.RobotoMono;
    public Vector2f Origin = new(0, 0);
    public Text RenderedText;

    // Public

    public override void Update()
    {
        base.Update();
        DrawText();
    }

    // Private

    private void DrawText()
    {
        RenderedText = new()
        {
            Font = Font,
            DisplayedString = Text,
            Position = GlobalPosition,
            CharacterSize = FontSize,
            Origin = Origin
        };

        RenderedText.Draw(Window, RenderStates.Default);
    }
}