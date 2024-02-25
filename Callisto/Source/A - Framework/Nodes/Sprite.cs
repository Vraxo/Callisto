using SFML.System;
using SFML.Graphics;

namespace Nodex;

class Sprite : Node
{
    // Fields

    public Vector2f Size;
    public Vector2f Origin;
    public Texture Texture;

    // Public

    public override void Update()
    {
        base.Update();

        RectangleShape r = new()
        {
            Position = Position,
            Size     = Size,
            Origin   = Origin,
            Texture  = Texture
        };

        r.Draw(Window, RenderStates.Default);
    }
}