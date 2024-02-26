using SFML.System;
using SFML.Graphics;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Texture Texture;

    private CircleShape circleShape = new();

    // Public

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        circleShape.Position = GlobalPosition;
        circleShape.Radius   = Radius;
        circleShape.Origin   = Origin;
        circleShape.Texture  = Texture;

        Window.Draw(circleShape);
    }
}