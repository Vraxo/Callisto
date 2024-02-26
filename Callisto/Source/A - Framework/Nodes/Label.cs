using SFML.System;
using SFML.Graphics;

namespace Nodex;

class Label : Node
{
    // Fields

    public string   Text     = "";
    public uint     FontSize = 16;
    public Font     Font     = FontLoader.Instance.Fonts["RobotoMono"];
    public Vector2f Origin   = new(0, 0);
    
    protected Text textRenderer = new();

    // Public

    public override void Update()
    {
        base.Update();
        DrawText();
    }

    // Private

    private void DrawText()
    {
        textRenderer.Position        = GlobalPosition;
        textRenderer.CharacterSize   = FontSize;
        textRenderer.Origin          = Origin;
        textRenderer.Font            = Font;
        textRenderer.DisplayedString = Text;

        Window.Draw(textRenderer);
    }
}