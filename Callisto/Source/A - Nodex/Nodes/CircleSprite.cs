using SFML.Graphics;
using SFML.System;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Texture Texture;
    public Action<CircleSprite> OnUpdate = (sprite) => { };

    private CircleShape circleRenderer = new();

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
        circleRenderer.Position = GlobalPosition;
        circleRenderer.Radius = Radius;
        circleRenderer.Origin = Origin;
        circleRenderer.Texture = Texture;

        Window.Draw(circleRenderer);
    }
}