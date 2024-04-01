using Nodex;
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

        AddChild(new Scroller
        {
            OnUpdate = (scroller) =>
            {
                float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
                scroller.CanGoUp = viewHeight > 0;

                float maxContactEditorHeight = GetNode<OkButton>("Buttons/OkButton").Position.Y + 50;
                float maxYPosition = maxContactEditorHeight - Window.GetView().Size.Y;
                scroller.CanGoDown = viewHeight < maxYPosition;
            }
        });
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
}