using Nodex;
using SFML.Graphics;

namespace Callisto.ContactDisplayerNode;

class CircleSprite : Nodex.CircleSprite
{
    // Fields

    public int ContactIndex = 0;

    // Public

    public override void Start()
    {
        base.Start();

        Radius = 20;
        Position = new(30, 25);
        Origin = new(Radius, Radius);
        Texture = GetTexture();
    }

    // Private

    private Texture GetTexture()
    {
        string photoPath = ContactsContainer.Instance.Contacts[ContactIndex].PhotoPath;

        Texture texture = photoPath == "" ?
                          TextureLoader.Instance.Textures["Avatar"] :
                          new(photoPath);

        return texture;
    }
}