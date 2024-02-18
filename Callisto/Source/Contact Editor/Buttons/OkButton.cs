using Nodex;
using SFML.Window;
using Callisto.NotificationDialogNode;
using Window = Nodex.Window;

namespace Callisto.ContactEditorNode.ButtonsNode;

class OkButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text          = "OK";
        ActionOnClick = ConfirmContact;
        Origin        = Size / 2;
        TextOrigin    = Size / 2;
    }

    public override void Update()
    {
        base.Update();

        float x = Window.Size.X / 2F;
        float y = GetParent<Buttons>().GetParent<ContactEditor>().GetChild<Fields>().NumberFields.Count * 50 + Window.Size.Y * 0.8F;

        Position = new(x, y);
    }

    // Callback

    private void ConfirmContact()
    {
        Contact newContact = GetParent<Buttons>().GetParent<ContactEditor>().GetChild<Fields>().GetContact();

        if (GetParent<Buttons>().Index == -1)
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
        Console.WriteLine(newContact.FirstName);

        if (newContact.FirstName != "")
        {
            if (!ContactsContainer.Instance.ContactExists(newContact))
            {
                ContactsContainer.Instance.Add(newContact);
                ChangeScene(new ContactsLoader());
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
            ContactsContainer.Instance.Contacts[GetParent<Buttons>().Index] = newContact;
            ContactsContainer.Instance.Save();
            ContactsContainer.Instance.Load();
            ChangeScene(new ContactsLoader());
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
}