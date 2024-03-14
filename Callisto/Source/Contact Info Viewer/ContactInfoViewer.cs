using Nodex;
using Callisto.AvatarDisplayerNode;
using Callisto.ContactsListNode;

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

        AddChild(new Button()
        {
            Text = "<-",
            OnClick = () =>
            {
                ChangeScene(new ContactsList());
            }
        }) ;

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