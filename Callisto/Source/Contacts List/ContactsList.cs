using Callisto.ContactDisplayerNode;
using Callisto.Properties;
using Nodex;
using SFML.Graphics;
using Image = SFML.Graphics.Image;

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

    private void CreateDirectories()
    {
        CreateResourcesDirectory();
        CreateDefaultFont();
        CreateDefaultAvatar();
        CreateIcon();
    }

    private void CreateResourcesDirectory()
    {
        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }
    }

    private void CreateDefaultFont()
    {
        string defaultFontPath = "Resources/RobotoMono.ttf";

        if (!File.Exists(defaultFontPath))
        {
            File.WriteAllBytes(defaultFontPath, Resources.RobotoMono);
        }
    }

    private void CreateDefaultAvatar()
    {
        string defaultAvatarPath = "Resources/DefaultAvatar.jpg";

        if (!File.Exists(defaultAvatarPath))
        {
            Resources.DefaultAvatar.Save(defaultAvatarPath);
        }
    }

    private void CreateIcon()
    {
        string iconPath = "Resources/Icon.jpg";

        if (!File.Exists(iconPath))
        {
            Resources.Icon.Save(iconPath);
        }

        Window.SetIcon(256, 256, new Image(iconPath).Pixels);
    }
}