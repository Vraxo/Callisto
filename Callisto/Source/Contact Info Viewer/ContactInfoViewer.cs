using Nodex;

namespace Callisto.ContactInfoViewerNode;

class ContactInfoViewer : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        AddChild(new AvatarDisplayer());

        AddChild(new ReturnButton());

        AddChild(new EditButton()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new NameLabel()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new NumberButtons()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new Scroller()
        {
            ContactIndex = ContactIndex
        });
    }
}