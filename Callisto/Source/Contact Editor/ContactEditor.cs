using Callisto.ContactEditorNode.ButtonsNode;
using Nodex;

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
            Index = ContactIndex
        };

        AddChild(buttons);

        AddChild(new Fields());
        AddChild(new AvatarDisplayer());
        AddChild(new Scroller());
    }

    public void DeleteContact()
    {
        ContactsContainer.Instance.Delete(ContactIndex);
        ChangeScene(new ContactsLoader());
    }
}