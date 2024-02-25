using Nodex;
using Callisto.ContactDisplayerNode;

namespace Callisto.ContactsListNode;

class ContactsList : Node
{
    // Fields

    private AddNewContactButton addNewContactButton;

    // Public

    public override void Start()
    {
        addNewContactButton = new();
        AddChild(addNewContactButton);

        CreateContactDisplayers();
        AddChild(new Scroller());
    }

    // Create nodes

    private void CreateContactDisplayers()
    {
        ContactsContainer.Instance.Load();

        List<Contact> contacts = ContactsContainer.Instance.Contacts;

        for (int i = 0; i < contacts.Count; i++)
        {
            ContactDisplayer contactDisplayer = new();
            contactDisplayer.ContactIndex = i;
            AddChild(contactDisplayer);

            var button = contactDisplayer.GetChild<ContactDisplayerNode.Button>();

            float x = 0;
            float y = addNewContactButton.Size.Y + button.Size.Y * i;

            contactDisplayer.Position = new(x, y);
        }
    }
}