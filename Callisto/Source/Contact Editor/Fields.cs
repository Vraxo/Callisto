using Nodex;
using Callisto.AvatarDisplayerNode;

namespace Callisto.ContactEditorNode;

class Fields : Node
{
    public int ContactIndex = -1;

    public List<ContactInfoField> NumberFields = [];

    private readonly int maximumCharacters = 29;

    private List<ContactInfoField> fields = [];

    private ContactInfoField firstNameField;
    private ContactInfoField lastNameField;

    private TextBox firstNameTextBox;
    private TextBox lastNameTextBox;
    private List<TextBox> numberTextBoxes = [];

    // Public

    public override void Start()
    {
        base.Start();

        CreateNameFields();
        CreateNumberFields();
        LoadContactInfoIntoFields();
    }

    public override void Update()
    {
        base.Update();
        
        UpdateFields();
        HandleExtraNumberFields();
    }

    public Contact GetContact()
    {
        string firstName = firstNameTextBox.Text;
        string lastName = lastNameTextBox.Text;
        List<string> phoneNumbers = GetPhoneNumbers();
        string photoPath = GetParent<ContactEditor>().GetChild<AvatarDisplayer>().PhotoPath;

        Contact newContact = new()
        {
            FirstName    = firstName,
            LastName     = lastName,
            PhoneNumbers = phoneNumbers,
            PhotoPath    = photoPath,
        };

        return newContact;
    }

    // Create nodes

    private void CreateNameFields()
    {
        firstNameField = CreateField("First Name");
        firstNameTextBox = firstNameField.GetChild<TextBox>();

        lastNameField = CreateField("Last Name");
        lastNameTextBox = lastNameField.GetChild<TextBox>();
    }

    private void CreateNumberFields()
    {
        if (ContactIndex != -1)
        {
            CreateExistingNumberFields();
        }
        else
        {
            CreateNewNumberField();
        }
    }

    private void CreateExistingNumberFields()
    {
        int phoneNumbersCount = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers.Count;

        for (int i = 0; i < phoneNumbersCount + 1; i ++)
        {
            ContactInfoField numberField = CreateField($"Phone Number {i + 1}");
            NumberFields.Add(numberField);
            numberTextBoxes.Add(numberField.GetChild<TextBox>());
        }
    }

    private void CreateNewNumberField()
    {
        ContactInfoField numberField = CreateField($"Phone Number 1");
        NumberFields.Add(numberField);
        numberTextBoxes.Add(numberField.GetChild<TextBox>());
    }

    private ContactInfoField CreateField(string labelText)
    {
        ContactInfoField field = new()
        {
            LabelText = labelText
        };

        AddChild(field, labelText.Replace(" ", ""));
        fields.Add(field);
        field.GetChild<TextBox>().MaximumCharacters = maximumCharacters;

        return field;
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

            for (int i = 0; i < contact.PhoneNumbers.Count; i++)
            {
                numberTextBoxes[i].Text = contact.PhoneNumbers[i];
            }
        }
    }

    private List<string> GetPhoneNumbers()
    {
        List<string> phoneNumbers = numberTextBoxes
            .Select(textBox => textBox.Text.Trim())
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        return phoneNumbers;
    }

    // Extra number fields

    private void HandleExtraNumberFields()
    {
        DeleteExtraNumberFields();
        CreateExtraNumberFields();
    }

    private void DeleteExtraNumberFields()
    {
        for (int i = numberTextBoxes.Count - 1; i > 0; i --)
        {
            if (numberTextBoxes[i].Text.Length == 0)
            {
                if (numberTextBoxes[i - 1].Text.Length == 0)
                {
                    RemoveFieldAndTextBoxAt(i);
                }
            }
        }
    }

    private void RemoveFieldAndTextBoxAt(int index)
    {
        Children.Remove(NumberFields[index]);
        fields.Remove(NumberFields[index]);
        NumberFields.RemoveAt(index);
        numberTextBoxes.RemoveAt(index);
    }

    private void CreateExtraNumberFields()
    {
        if (!string.IsNullOrWhiteSpace(numberTextBoxes.Last().Text))
        {
            ContactInfoField numberField = CreateField($"Phone Number {NumberFields.Count + 1}");
            NumberFields.Add(numberField);
            numberTextBoxes.Add(numberField.GetChild<TextBox>());
        }
    }
}