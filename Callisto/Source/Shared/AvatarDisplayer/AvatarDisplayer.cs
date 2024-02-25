using Nodex;

namespace Callisto.AvatarDisplayerNode;

class AvatarDisplayer : Node
{
    // Fields

    public int ContactIndex = -1;
    public bool IsClickable = false;
    public string PhotoPath = "";

    // Public

    public override void Start()
    {
        PhotoPath = ContactIndex == -1 ?
                    "" :
                    ContactsContainer.Instance.Contacts[ContactIndex].PhotoPath;

        AddChild(new CircleSprite()
        {
            Texture = PhotoPath == "" ?
                      TextureLoader.Instance.Textures["Avatar"] :
                      new(PhotoPath)
        });

        if (IsClickable)
        {
            AddChild(new CircleButton());
        }
    }
}