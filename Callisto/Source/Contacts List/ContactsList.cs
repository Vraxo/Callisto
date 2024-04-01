using Callisto.ContactDisplayerNode;
using Nodex;

namespace Callisto.ContactsListNode;

class ContactsList : Node
{
    // AllFields

    private AddNewContactButton addNewContactButton;

    // Public

    public override void Start()
    {
        new Initializer().Initialize();

        ContactsContainer.Instance.Load();
        AvatarLoader.Instance.Load();

        addNewContactButton = new AddNewContactButton();
        AddChild(addNewContactButton);

        CreateContactDisplayers();
        AddChild(new Scroller());
    }

    // Create nodes

    private void CreateContactDisplayers()
    {
        List<Contact> contacts = ContactsContainer.Instance.Contacts;

        for (int i = 0; i < contacts.Count; i++)
        {
            ContactDisplayer contactDisplayer = new()
            {
                ContactIndex = i
            };

            AddChild(contactDisplayer, $"ContactDisplayer{i}");

            var button = contactDisplayer.GetChild<Button>();

            float x = 0;
            float y = addNewContactButton.Size.Y + button.Size.Y * i;

            contactDisplayer.Position = new(x, y);
        }
    }
}