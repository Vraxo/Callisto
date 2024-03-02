using Nodex;

namespace Callisto.AvatarDisplayerNode;

class Sprite : CircleSprite
{
    // Public

    public override void Start()
    {
        Radius = 100;
        Origin = new(Radius, Radius);
        Texture = TextureLoader.Instance.Textures["Avatar"];
    }

    public override void Update()
    {
        base.Update();

        Position = new(Window.Size.X / 2, Window.Size.Y * 0.2F);
    }
}