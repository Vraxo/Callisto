using SFML.System;
using SFML.Graphics;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Texture Texture;
    public Action<CircleSprite> OnUpdate = (sprite) => { };

    // Public

    public override void Update()
    {
        base.Update();

        Draw();
        OnUpdate(this);
    }

    // Private

    private void Draw()
    {
        Window.Draw(new CircleShape
        {
            Position = GlobalPosition,
            Radius = Radius,
            Origin = Origin,
            Texture = Texture
        });
    }
}