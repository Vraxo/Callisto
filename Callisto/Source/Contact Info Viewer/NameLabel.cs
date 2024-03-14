using Nodex;

namespace Callisto.ContactInfoViewerNode;

class NameLabel : Label
{
    // Fields

    public int ContactIndex = -1;

    private const int Factor = 50;

    private const maxFontSize = 32;

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
        float halfNameLength = TextRenderer.GetLocalBounds().Width / 2;

        Position.X = windowCenter - halfNameLength;
        Position.Y = 0.4F * Window.Size.Y;
    }

    private void UpdateFontSize()
    {
        if (TextRenderer.GetLocalBounds().Width > Window.Size.X - Factor)
        {
            FontSize --;
            TextRenderer.CharacterSize = FontSize;
        }
        else
        {
            uint currentSize = FontSize;

            FontSize++;

            if (FontSize > maxFontSize)
            {
                FontSize = maxFontSize;
            }

            TextRenderer.CharacterSize = FontSize;

            if (TextRenderer.GetLocalBounds().Width > Window.Size.X - Factor)
            {
                FontSize = currentSize;
                TextRenderer.CharacterSize = FontSize;
            }
        }
    }
}