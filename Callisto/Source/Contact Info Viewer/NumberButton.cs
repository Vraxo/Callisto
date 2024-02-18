using Nodex;
using SFML.Window;

namespace Callisto.ContactInfoViewerNode;

class NumberButton : Button
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Position         = new(0, Window.Size.Y * 0.55F);
        Size             = new(270, 50);
        OutlineThickness = 0;
        FontSize         = 24;
        ActionOnClick    = CopyPhoneNumber;
    }

    // Callback

    private void CopyPhoneNumber()
    {
        Clipboard.Contents = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers[0];
    }
}