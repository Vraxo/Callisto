using SFML.Graphics;
using SFML.System;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Texture Texture;

    private CircleShape circleRenderer = new();

    // Public

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        circleRenderer.Position = GlobalPosition;
        circleRenderer.Radius = Radius;
        circleRenderer.Origin = Origin;
        circleRenderer.Texture = Texture;

        Window.Draw(circleRenderer);
    }
}