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
    public Text Renderer = new();

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
        Renderer.Position = GlobalPosition;
        Renderer.CharacterSize = FontSize;
        Renderer.Origin = Origin;
        Renderer.Font = Font;
        Renderer.DisplayedString = Text;

        Window.Draw(Renderer);
    }
}