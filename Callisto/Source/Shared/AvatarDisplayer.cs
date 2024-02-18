using Nodex;
using SFML.Graphics;

namespace Callisto;

class AvatarDisplayer : Node
{
    // Fields

    public Texture Avatar = TextureLoader.Instance.Person;

    public override void Update()
    {
        base.Update();

        Position = new(Window.Size.X / 2, Window.Size.Y * 0.2F);

        CircleShape c = new()
        {
            Texture  = Avatar,
            Position = Position,
            Radius   = 100,
            Origin   = new(100, 100)
        };

        c.Draw(Window, RenderStates.Default);
    }
}