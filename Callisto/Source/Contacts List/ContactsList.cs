using Callisto.ContactDisplayerNode;
using Callisto.Properties;
using Nodex;

namespace Callisto.ContactsListNode;

class ContactsList : Node
{
    // Fields

    private AddNewContactButton addNewContactButton;

    // Public

    public override void Start()
    {
        CreateDirectories();

        ContactsContainer.Instance.Load();

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

    private void CreateDirectories()
    {
        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }

        string defaultFontPath = "Resources/RobotoMono.ttf";

        if (!File.Exists(defaultFontPath))
        {
            File.WriteAllBytes(defaultFontPath, Resources.RobotoMono);
        }

        string defaultAvatarPath = "Resources/DefaultAvatar.jpg";

        if (!File.Exists(defaultAvatarPath))
        {
            Resources.DefaultAvatar.Save(defaultAvatarPath);
        }
    }
}