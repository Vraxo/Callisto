using Nodex;
using Callisto.AvatarDisplayerNode;

namespace Callisto.ContactInfoViewerNode;

class ContactInfoViewer : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        AddChild(new AvatarDisplayer()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new ReturnButton());

        AddChild(new EditButton()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new NameLabel()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new CopyNumberButtons()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new Scroller()
        {
            ContactIndex = ContactIndex
        });
    }
}