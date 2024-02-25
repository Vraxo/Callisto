using Nodex;
using Callisto.AvatarDisplayerNode;
using Callisto.ContactsListNode;
using Callisto.ContactEditorNode.ButtonsNode;

namespace Callisto.ContactEditorNode;

class ContactEditor : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        Buttons buttons = new()
        {
            ContactIndex = ContactIndex
        };

        AddChild(buttons);

        AddChild(new Fields()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new AvatarDisplayer()
        {
            ContactIndex = ContactIndex,
            IsClickable  = true
        });

        AddChild(new Scroller());
    }

    public void DeleteContact()
    {
        ContactsContainer.Instance.Delete(ContactIndex);
        ChangeScene(new ContactsList());
    }
}