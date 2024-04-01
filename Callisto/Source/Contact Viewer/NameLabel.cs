using Nodex;

namespace Callisto.ContactInfoViewerNode;

class NameLabel : Label
{
    // AllFields

    public int ContactIndex = -1;

    private const uint MaxFontSize = 32;

    // Public

    public override void Start()
    {
        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];
        string fullName = contact.GetFullName();

        Text = fullName;
        FontSize = 32;
        Position = new(0, 200);
    }

    public override void Update()
    {
        base.Update();

        UpdatePosition();
        UpdateFontSize();
    }

    // Private

    private void UpdatePosition()
    {
        float windowCenter = Window.Size.X / 2;
        float halfNameLength = Renderer.GetLocalBounds().Width / 2;

        Position.X = windowCenter - halfNameLength;
        Position.Y = 0.4F * Window.Size.Y;
    }

    private void UpdateFontSize()
    {
        FontSize = MaxFontSize;
        Renderer.CharacterSize = FontSize;

        while (Renderer.GetLocalBounds().Width > Window.Size.X)
        {
            FontSize --;
            Renderer.CharacterSize = FontSize;
        }
    }
}