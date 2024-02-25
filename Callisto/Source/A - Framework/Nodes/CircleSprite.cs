using SFML.System;
using SFML.Graphics;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Texture Texture;

    // Public

    public override void Update()
    {
        base.Update();

        CircleShape c = new()
        {
            Position = GlobalPosition,
            Radius   = Radius,
            Origin   = Origin,
            Texture  = Texture
        };

        c.Draw(Window, RenderStates.Default);
    }
}