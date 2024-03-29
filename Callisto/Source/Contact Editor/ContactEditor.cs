﻿using Callisto.AvatarDisplayerNode;
using Callisto.ContactEditorNode.ButtonsNode;
using Callisto.ContactsListNode;
using Nodex;

namespace Callisto.ContactEditorNode;

class ContactEditor : Node
{
    // Fields

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

        AddChild(new VerticalViewScroller());
    }

    public void DeleteContact()
    {
        ContactsContainer.Instance.Delete(ContactIndex);
        ChangeScene(new ContactsList());
    }

    public Contact GetContact()
    {
        int id = ContactIndex == -1 ? Contact.GenerateUniqueId() : ContactsContainer.Instance.Contacts[ContactIndex].Id;
        string firstName = fields.FirstNameTextBox.Text;
        string lastName = fields.LastNameTextBox.Text;
        List<string> phoneNumbers = fields.GetChild<PhoneNumberFields>().GetPhoneNumbers();
        bool hasAvatar = GetNode<AvatarDisplayerNode.CircleSprite>("AvatarDisplayer/CircleSprite").Texture != TextureLoader.Instance.Textures["Avatar"];

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
}