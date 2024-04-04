using SFML.System;
using SFML.Graphics;

namespace Nodex;

class CircleSprite : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public Action<CircleSprite> OnUpdate = (sprite) => { };

    private CircleShape circleRenderer = new();

    // Properties

    private Texture texture;

    public Texture Texture
    {
        get
        {
            return texture;
        }

        set
        {
            texture = value;
            circleRenderer = new();
        }
    }

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
        circleRenderer.Texture = texture;

        Window.Draw(circleRenderer);
    }
}