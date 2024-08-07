﻿using Callisto;
using Callisto.ContactEditorNode.ButtonsNode;
using Callisto.ContactsListNode;

namespace Callisto.ContactEditorNode;

class ContactEditor : Node
{
    // AllFields

    public int ContactIndex = -1;

    private Fields fields;

    // Public

    public override void Start()
    {
        AddChild(new Buttons
        {
            ContactIndex = ContactIndex
        });

        fields = new Fields
        { 
            ContactIndex = ContactIndex 
        };

        AddChild(fields);

        AddChild(new AvatarDisplayer
        {
            ContactIndex = ContactIndex,
            IsClickable = true
        });

        AddScroller();
    }

    public void DeleteContact()
    {
        DeleteContactAvatar();
        ContactsContainer.Instance.Delete(ContactIndex);
        ChangeScene(new ContactsList());
    }

    public Contact GetContact()
    {
        int id = ContactIndex == -1 ? Contact.GenerateUniqueId() : ContactsContainer.Instance.Contacts[ContactIndex].Id;
        string firstName = fields.FirstNameTextBox.Text;
        string lastName = fields.LastNameTextBox.Text;
        List<string> phoneNumbers = fields.GetChild<PhoneNumberFields>().GetPhoneNumbers();
        bool hasAvatar = GetNode<CircleSprite>("AvatarDisplayer/CircleSprite").Texture != TextureLoader.Instance.Textures["Avatar"];

        Contact newContact = new()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumbers = phoneNumbers,
            HasAvatar = hasAvatar
        };

        return newContact;
    }

    // Private

    private void DeleteContactAvatar()
    {
        int contactId = ContactsContainer.Instance.Contacts[ContactIndex].Id;
        string avatarsFolder = Path.Combine("Resources/Avatars/");
        string[] avatarFiles = Directory.GetFiles(avatarsFolder, $"{contactId}.*");

        foreach (var file in avatarFiles)
        {
            File.Delete(file);
        }
    }

    private void AddScroller()
    {
        AddChild(new VerticalViewScroller
        {
            OnUpdate = (scroller) =>
            {
                float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
                float maxContactEditorHeight = GetNode<Button>("Buttons/OkButton").Position.Y + 50;
                float maxYPosition = maxContactEditorHeight - Window.GetView().Size.Y;
                scroller.CanGoDown = viewHeight < maxYPosition;
            }
        });
    }
}