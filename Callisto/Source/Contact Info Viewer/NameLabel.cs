using Nodex;

namespace Callisto.ContactInfoViewerNode;

class NameLabel : Label
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];
        string fullName = contact.GetFullName();

        Text     = fullName;
        FontSize = 32;
        Position = new(0, 200);
    }

    public override void Update()
    {
        base.Update();

        float windowCenter = Window.Size.X / 2;
        float halfNameLength = RenderedText.GetLocalBounds().Width / 2;

        Position.X = windowCenter - halfNameLength;
        Position.Y = 0.4F * Window.Size.Y;
    }
}