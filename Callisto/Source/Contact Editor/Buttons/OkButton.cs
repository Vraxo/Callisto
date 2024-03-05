using Callisto.AvatarDisplayerNode;
using Callisto.ContactInfoViewerNode;
using Callisto.NotificationDialogNode;
using Nodex;
using SFML.Window;
using Window = Nodex.Window;

namespace Callisto.ContactEditorNode.ButtonsNode;

class OkButton : Button
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Text = "OK";
        actionOnClick = ConfirmContact;
        Origin = Size / 2;
        TextOrigin = Size / 2;
    }

    public override void Update()
    {
        base.Update();

        float x = Window.Size.X / 2F;
        float y = GetNode<PhoneNumberFields>("Fields/PhoneNumberFields").Fields.Count * 50 + Window.Size.Y * 0.8F;

        Position = new(x, y);
    }

    // Callback

    private void ConfirmContact()
    {
        Contact newContact = GetNode<ContactEditor>("").GetContact();

        if (GetParent<Buttons>().ContactIndex == -1)
        {
            CreateNewContact(newContact);
        }
        else
        {
            EditExistingContact(newContact);
        }
    }

    private void CreateNewContact(Contact newContact)
    {
        if (newContact.FirstName != "")
        {
            if (!ContactsContainer.Instance.ContactExists(newContact))
            {
                ContactsContainer.Instance.Add(newContact);

                ContactIndex = ContactsContainer.Instance.Contacts.IndexOf(newContact);

                SaveAvatar();

                ContactsContainer.Instance.Save();
                ContactsContainer.Instance.Load();

                ChangeScene(new ContactInfoViewer()
                {
                    ContactIndex = ContactIndex
                });
            }
            else
            {
                CreateNotificationDialog("A contact with the same name already\nexists.");
            }
        }
        else
        {
            CreateNotificationDialog("The first name cannot be empty.");
        }
    }

    private void EditExistingContact(Contact newContact)
    {
        if (newContact.FirstName != "")
        {
            ContactsContainer.Instance.Contacts[ContactIndex] = newContact;

            SaveAvatar();

            Console.WriteLine();

            ContactsContainer.Instance.Save();
            ContactsContainer.Instance.Load();

            ChangeScene(new ContactInfoViewer()
            {
                ContactIndex = ContactIndex
            });
        }
        else
        {
            CreateNotificationDialog("The first name cannot be empty.");
        }
    }

    private void CreateNotificationDialog(string message)
    {
        NotificationDialog notificationDialog = new()
        {
            Message = message
        };

        Window window = new
        (
            new(360, 120),
            "Invalid first name",
            Styles.Close,
            new ContextSettings(0, 0, 16)
        )
        {
            RootNode = notificationDialog
        };

        window.Start();
        Program.AddWindow(window);
    }

    private void SaveAvatar()
    {
        string imagePath = GetNode<AvatarDisplayer>("AvatarDisplayer").ImagePath;

        if (imagePath != "")
        {
            if (!Directory.Exists("Resouces/Avatars"))
            {
                Directory.CreateDirectory("Resources/Avatars");
            }

            string extension = Path.GetExtension(imagePath);
            string contactId = ContactsContainer.Instance.Contacts[ContactIndex].Id.ToString();
            string avatarPath = $"Resources/Avatars/{contactId}{extension}";

            File.Copy(imagePath, avatarPath, true);
            TextureLoader.Instance.Textures[contactId] = new(imagePath);
        }
    }
}