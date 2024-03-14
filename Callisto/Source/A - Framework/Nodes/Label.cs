using SFML.System;
using SFML.Graphics;

namespace Nodex;

class Label : Node
{
    // Fields

    public string Text = "";
    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.Fonts["RobotoMono"];
    public Vector2f Origin = new(0, 0);
    public Action<Label> OnUpdate = (label) => { };
    public Text TextRenderer = new();

    // Public

    public override void Update()
    {
        base.Update();
        OnUpdate(this);
        DrawText();
    }

    // Private

    private void DrawText()
    {
        TextRenderer.Position = GlobalPosition;
        TextRenderer.CharacterSize = FontSize;
        TextRenderer.Origin = Origin;
        TextRenderer.Font = Font;
        TextRenderer.DisplayedString = Text;

        Window.Draw(TextRenderer);
    }
}