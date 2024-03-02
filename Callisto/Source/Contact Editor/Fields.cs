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
    private List<TextBox> phoneNumberTextBoxes = [];

    #endregion

    // Public

    public override void Start()
    {
        base.Start();

        CreateNameFields();
        CreatePhoneNumberFields();
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

        Contact newContact = new()
        {
            Id = ContactIndex == -1 ? new Random().Next() : ContactsContainer.Instance.Contacts[ContactIndex].Id,
            FirstName = firstName,
            LastName = lastName,
            PhoneNumbers = phoneNumbers,
            HasAvatar = GetParent<ContactEditor>().GetChild<AvatarDisplayer>().ImagePath != ""
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

    private void CreatePhoneNumberFields()
    {
        if (ContactIndex != -1)
        {
            CreateExistingPhoneNumberFields();
        }
        else
        {
            CreateNewPhoneNumberField();
        }
    }

    private void CreateExistingPhoneNumberFields()
    {
        int phoneNumbersCount = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers.Count;

        for (int i = 0; i < phoneNumbersCount + 1; i ++)
        {
            ContactInfoField numberField = CreateField($"Phone Number {i + 1}");
            NumberFields.Add(numberField);
            phoneNumberTextBoxes.Add(numberField.GetChild<TextBox>());
            phoneNumberTextBoxes.Last().AllowedCharacters = "0123456789".ToCharArray().ToList();
        }
    }

    private void CreateNewPhoneNumberField()
    {
        ContactInfoField numberField = CreateField($"Phone Number 1");
        NumberFields.Add(numberField);
        phoneNumberTextBoxes.Add(numberField.GetChild<TextBox>());
    }

    private ContactInfoField CreateField(string labelText)
    {
        ContactInfoField field = new()
        {
            LabelText = labelText
        };

        AddChild(field, labelText.Replace(" ", ""));
        fields.Add(field);
        field.GetChild<TextBox>().MaximumCharacters = MaximumCharacters;

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
                phoneNumberTextBoxes[i].Text = contact.PhoneNumbers[i];
            }
        }
    }

    private List<string> GetPhoneNumbers()
    {
        List<string> phoneNumbers = phoneNumberTextBoxes
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
        for (int i = phoneNumberTextBoxes.Count - 1; i > 0; i--)
        {
            if (phoneNumberTextBoxes[i].Text.Length == 0)
            {
                if (phoneNumberTextBoxes[i - 1].Text.Length == 0)
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
        phoneNumberTextBoxes.RemoveAt(index);
    }

    private void CreateExtraNumberFields()
    {
        if (!string.IsNullOrWhiteSpace(phoneNumberTextBoxes.Last().Text))
        {
            string labelText = $"Phone Number {NumberFields.Count + 1}";

            ContactInfoField numberField = CreateField(labelText);
            NumberFields.Add(numberField);
            phoneNumberTextBoxes.Add(numberField.GetChild<TextBox>());
        }
    }
}