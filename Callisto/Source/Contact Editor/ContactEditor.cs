using Callisto.AvatarDisplayerNode;
using Callisto.ContactEditorNode.ButtonsNode;
using Callisto.ContactsListNode;
using Nodex;

namespace Callisto.ContactEditorNode;

class ContactEditor : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        AddChild(new Buttons()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new Fields()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new AvatarDisplayer()
        {
            ContactIndex = ContactIndex,
            IsClickable = true
        });

        AddChild(new Scroller());
    }

    public void DeleteContact()
    {
        ContactsContainer.Instance.Delete(ContactIndex);
        ChangeScene(new ContactsList());
    }
}