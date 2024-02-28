using Nodex;
using Callisto.ContactDisplayerNode;

namespace Callisto.ContactsListNode;

class ContactsList : Node
{
    // Fields

    private AddNewContactButton addNewContactButton;

    private int counter = 0;

    // Public

    public override void Start()
    {
        ContactsContainer.Instance.Load();

        addNewContactButton = new();
        AddChild(addNewContactButton);

        //CreateContactDisplayers();
        AddChild(new Scroller());
    }

    public override void Update()
    {
        base.Update();

        int numberOfContacts = ContactsContainer.Instance.Contacts.Count;

        if (counter < numberOfContacts)
        {
            for (int i = 0; i < 1; i++)
            {
                ContactDisplayer contactDisplayer = new()
                {
                    ContactIndex = counter + i
                };

                AddChild(contactDisplayer, $"ContactDisplayer{counter + i}");

                var button = contactDisplayer.GetChild<ContactDisplayerNode.Button>();

                float x = 0;
                float y = addNewContactButton.Size.Y + button.Size.Y * (counter + i);

                contactDisplayer.Position = new(x, y);

                contactDisplayer.Update();
            }

            counter += 1;
        }
    }

    // Create nodes

    private async void CreateContactDisplayers()
    {
        List<Contact> contacts = ContactsContainer.Instance.Contacts;

        for (int i = 0; i < contacts.Count; i++)
        {
            ContactDisplayer contactDisplayer = new()
            {
                ContactIndex = i
            };

            AddChild(contactDisplayer, $"ContactDisplayer{i}");

            var button = contactDisplayer.GetChild<ContactDisplayerNode.Button>();

            float x = 0;
            float y = addNewContactButton.Size.Y + button.Size.Y * i;

            contactDisplayer.Position = new(x, y);
        }
    }
}