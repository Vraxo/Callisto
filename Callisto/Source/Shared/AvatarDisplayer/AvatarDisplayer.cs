using HarfBuzzSharp;
using Nodex;
using SFML.Graphics;

namespace Callisto.AvatarDisplayerNode;

class AvatarDisplayer : Node
{
    // Fields

    public int ContactIndex = -1;
    public bool IsClickable = false;
    public string ImagePath = "";

    // Public

    public override void Start()
    {
        AddChild(new CircleSprite()
        {
            Texture = GetTexture() 
        });

        if (IsClickable)
        {
            AddChild(new CircleButton()
            {
                ContactIndex = ContactIndex
            });
        }
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