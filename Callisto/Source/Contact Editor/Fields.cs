using Nodex;
using Callisto.AvatarDisplayerNode;

namespace Callisto.ContactEditorNode;

class Fields : Node
{
    #region [ - - - Fields - - - ]

    public int ContactIndex = -1;
    public List<ContactInfoField> NumberFields = [];
    public int MaximumCharacters = 29;
    public TextBox FirstNameTextBox;
    public TextBox LastNameTextBox;

    private List<ContactInfoField> fields = [];
    private ContactInfoField firstNameField;
    private ContactInfoField lastNameField;

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



    // Create nodes

    private void CreateNameFields()
    {
        firstNameField = new ContactInfoField()
        {
            LabelText = "First Name"
        };

        AddChild(firstNameField);
        fields.Add(firstNameField);
        FirstNameTextBox = firstNameField.GetChild<TextBox>();

        lastNameField = new ContactInfoField()
        {
            LabelText = "Last Name"
        };

        AddChild(lastNameField);
        fields.Add(lastNameField);
        LastNameTextBox = lastNameField.GetChild<TextBox>();
    }

    // Private

    private void UpdateFields()
    {
        float fieldX = (Window.Size.X / 2) - (FirstNameTextBox.Size.X / 2);

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

            FirstNameTextBox.Text = contact.FirstName;
            LastNameTextBox.Text = contact.LastName;
        }
    }
}