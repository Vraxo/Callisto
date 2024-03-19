using Nodex;

namespace Callisto.ContactEditorNode;

class PhoneNumberFields : Node
{
    // Field

    public int ContactIndex = -1;
    public int MaxCharacters = 25;

    public List<ContactInfoField> Fields = [];
    private List<TextBox> textBoxes = [];

    // Public

    public override void Start()
    {
        base.Start();

        if (ContactIndex != -1)
        {
            CreateExistingPhoneNumberFields();
        }
        else
        {
            CreateNewPhoneNumberField();
        }
    }

    public override void Update()
    {
        base.Update();

        UpdateFields();
        CreateExtraNumberFields();
        DeleteExtraNumberFields();
    }

    public List<string> GetPhoneNumbers()
    {
        List<string> phoneNumbers = textBoxes
            .Select(textBox => textBox.Text.Trim())
            .Where(text => !string.IsNullOrWhiteSpace(text))
            .ToList();

        return phoneNumbers;
    }

    // Private

    private void UpdateFields()
    {
        float fieldX = (Window.Size.X / 2) - (textBoxes[0].Size.X / 2);

        for (int i = 0; i < Fields.Count; i++)
        {
            Fields[i].Position = new(fieldX, Window.Size.Y * (0.4F + ((i + 2) * 0.1F)));
        }
    }

    private void CreateExistingPhoneNumberFields()
    {
        int phoneNumbersCount = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers.Count;

        for (int i = 0; i < phoneNumbersCount + 1; i++)
        {
            CreateNumberField($"Phone Number {i + 1}", i);
        }
    }

    private void CreateNewPhoneNumberField()
    {
        ContactInfoField numberField = CreateField($"Phone Number 1");
        textBoxes.Add(numberField.GetChild<TextBox>());
    }

    private ContactInfoField CreateField(string labelText)
    {
        ContactInfoField field = new()
        {
            LabelText = labelText
        };

        AddChild(field, labelText.Replace(" ", ""));
        field.GetChild<TextBox>().MaxCharacters = MaxCharacters;
        Fields.Add(field);

        return field;
    }

    private void CreateNumberField(string labelText, int index = -1)
    {
        ContactInfoField numberField = CreateField(labelText);
        textBoxes.Add(numberField.GetChild<TextBox>());
        textBoxes.Last().AllowedCharacters = "0123456789".ToCharArray().ToList();

        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];
        int phoneNumbersCount = contact.PhoneNumbers.Count;

        if (index != -1 && index < phoneNumbersCount)
        {
            textBoxes.Last().Text = contact.PhoneNumbers[index];
        }
    }

    private void CreateExtraNumberFields()
    {
        if (!string.IsNullOrWhiteSpace(textBoxes.Last().Text))
        {
            CreateNumberField($"Phone Number {Fields.Count + 1}");
        }
    }

    private void DeleteExtraNumberFields()
    {
        for (int i = textBoxes.Count - 1; i > 0; i--)
        {
            if (textBoxes[i].Text.Length == 0)
            {
                if (textBoxes[i - 1].Text.Length == 0)
                {
                    RemoveFieldAndTextBoxAt(i);
                }
            }
        }
    }

    private void RemoveFieldAndTextBoxAt(int index)
    {
        Children.Remove(Fields[index]);
        Fields.Remove(Fields[index]);
        textBoxes.RemoveAt(index);
    }
}