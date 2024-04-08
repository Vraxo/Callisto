using Callisto;
using SFML.Graphics;

namespace Callisto.ContactDisplayerNode;

class CircleSprite : Callisto.CircleSprite
{
    // AllFields

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
        Texture texture = TextureLoader.Instance.Textures["Avatar"];

        if (ContactIndex != -1)
        {
            Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];

            if (contact.HasAvatar)
            {
                texture = TextureLoader.Instance.Textures[contact.Id.ToString()];
            }
        }

        return texture;
    }
}