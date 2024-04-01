using Nodex;

namespace Callisto.ContactEditorNode;

class Fields : Node
{
    #region [ - - - Fields - - - ]

    public int ContactIndex = -1;
    public List<ContactInfoField> NumberFields = [];
    public int MaxCharacters = 29;
    public TextBox FirstNameTextBox;
    public TextBox LastNameTextBox;

    public List<ContactInfoField> AllFields = [];
    private ContactInfoField firstNameField;
    private ContactInfoField lastNameField;

    #endregion

    // Public

    public override void Start()
    {
        base.Start();

        CreateNameFields();
        
        AddChild(new PhoneNumberFields
        {
            ContactIndex = ContactIndex,
        });

        LoadContactInfoIntoNameFields();
    }

    public override void Update()
    {
        base.Update();

        UpdateFields();
    }

    // Create nodes

    private void CreateNameFields()
    {
        firstNameField = new ContactInfoField()
        {
            LabelText = "First Name"
        };

        AddChild(firstNameField);
        AllFields.Add(firstNameField);
        FirstNameTextBox = firstNameField.GetChild<TextBox>();
        FirstNameTextBox.MaxCharacters = MaxCharacters;

        lastNameField = new ContactInfoField()
        {
            LabelText = "Last Name"
        };

        AddChild(lastNameField);
        AllFields.Add(lastNameField);
        LastNameTextBox = lastNameField.GetChild<TextBox>();
        LastNameTextBox.MaxCharacters = MaxCharacters;
    }

    // Private

    private void UpdateFields()
    {
        float fieldX = (Window.Size.X / 2) - (FirstNameTextBox.Size.X / 2);

        for (int i = 0; i < AllFields.Count; i ++)
        {
            AllFields[i].Position = new(fieldX, 250 + i * FirstNameTextBox.Size.Y * 2.5F);
            //AllFields[i].Position = new(fieldX, Window.Size.Y * (0.4F + (i * 0.1F)));
        }
    }

    private void LoadContactInfoIntoNameFields()
    {
        var index = GetParent<ContactEditor>().ContactIndex;

        if (index != -1)
        {
            Contact contact = ContactsContainer.Instance.Contacts[index];

            FirstNameTextBox.Text = contact.FirstName;
            LastNameTextBox.Text = contact.LastName;
        }
    }
}