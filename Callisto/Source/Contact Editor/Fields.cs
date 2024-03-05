using Nodex;
using Callisto.AvatarDisplayerNode;

namespace Callisto.ContactEditorNode;

class Fields : Node
{
    #region [ - - - Fields - - - ]

    public int ContactIndex = -1;
    public List<ContactInfoField> NumberFields = [];
    public int MaximumCharacters = 29;
    
    private List<ContactInfoField> fields = [];
    private ContactInfoField firstNameField;
    private ContactInfoField lastNameField;
    private TextBox firstNameTextBox;
    private TextBox lastNameTextBox;

    #endregion

    // Public

    public override void Start()
    {
        base.Start();

        CreateNameFields();
        
        AddChild(new PhoneNumberFields()
        {
            ContactIndex = ContactIndex,
        });

        LoadContactInfoIntoFields();
    }

    public override void Update()
    {
        base.Update();

        UpdateFields();
    }

    public Contact GetContact()
    {
        string firstName = firstNameTextBox.Text;
        string lastName = lastNameTextBox.Text;
        List<string> phoneNumbers = GetChild<PhoneNumberFields>().GetPhoneNumbers();
        int id = ContactIndex == -1 ? Contact.GenerateUniqueId() : ContactsContainer.Instance.Contacts[ContactIndex].Id;

        Contact newContact = new()
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumbers = phoneNumbers,
            HasAvatar = GetNode<AvatarDisplayer>("AvatarDisplayer").ImagePath != ""
        };

        return newContact;
    }

    // Create nodes

    private void CreateNameFields()
    {
        firstNameField = new ContactInfoField()
        {
            LabelText = "First Name"
        };

        AddChild(firstNameField);
        fields.Add(firstNameField);
        firstNameTextBox = firstNameField.GetChild<TextBox>();

        lastNameField = new ContactInfoField()
        {
            LabelText = "Last Name"
        };

        AddChild(lastNameField);
        fields.Add(lastNameField);
        lastNameTextBox = lastNameField.GetChild<TextBox>();
    }

    // Private

    private void UpdateFields()
    {
        float fieldX = (Window.Size.X / 2) - (firstNameTextBox.Size.X / 2);

        for (int i = 0; i < fields.Count; i++)
        {
            fields[i].Position = new(fieldX, Window.Size.Y * (0.4F + (i * 0.1F)));
        }
    }

    private void LoadContactInfoIntoFields()
    {
        var index = GetParent<ContactEditor>().ContactIndex;

        if (index != -1)
        {
            Contact contact = ContactsContainer.Instance.Contacts[index];

            firstNameTextBox.Text = contact.FirstName;
            lastNameTextBox.Text = contact.LastName;
        }
    }
}