using Nodex;

namespace Callisto.ContactEditorNode;

class PhoneNumberFields : Node
{
    // Field

    public int ContactIndex = -1;
    public int MaxCharacters = 13;
    public List<ContactInfoField> Fields = [];

    private List<TextBox> textBoxes = [];

    // Public

    public override void Start()
    {
        base.Start();

        if (ContactIndex != -1)
        {
            AddExistingPhoneNumberFields();
        }
        else
        {
            AddPhoneNumberField("Phone Number 1");
        }
    }

    public override void Update()
    {
        base.Update();

        AddExtraNumberFields();
        DeleteExtraNumberFields();
    }

    public List<string> GetPhoneNumbers()
    {
        List<string> phoneNumbers = textBoxes
            .Select(textBox => textBox.Text.Trim())
            .Where(text => !string.IsNullOrWhiteSpace(text.Substring(1)))
            .ToList();

        return phoneNumbers;
    }

    // Private

    private void AddExistingPhoneNumberFields()
    {
        int phoneNumbersCount = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers.Count;

        for (int i = 0; i < phoneNumbersCount + 1; i++)
        {
            AddPhoneNumberField($"Phone Number {i + 1}", i);
        }
    }

    private void AddPhoneNumberField(string labelText, int index = -1)
    {
        ContactInfoField numberField = AddField(labelText);
        textBoxes.Add(numberField.GetChild<TextBox>());
        textBoxes.Last().AllowedCharacters = CharacterSet.Numbers;
        textBoxes.Last().Text = "+";
        textBoxes.Last().MinCharacters = 1;

        if (ContactIndex == -1) return;

        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];
        int phoneNumbersCount = contact.PhoneNumbers.Count;

        if (index != -1 && index < phoneNumbersCount)
        {
            textBoxes.Last().Text = contact.PhoneNumbers[index];
        }
    }

    // Creating and destroying fields

    private ContactInfoField AddField(string labelText)
    {
        ContactInfoField field = new()
        {
            LabelText = labelText
        };

        AddChild(field, labelText.Replace(" ", ""));
        field.GetChild<TextBox>().MaxCharacters = MaxCharacters;
        Fields.Add(field);
        GetParent<Fields>().AllFields.Add(field);

        return field;
    }

    private void RemoveFieldAndTextBoxAt(int index)
    {
        GetParent<Fields>().AllFields.Remove(Fields[index]);
        Children.Remove(Fields[index]);
        Fields.Remove(Fields[index]);
        textBoxes.RemoveAt(index);
    }

    // Extra fields

    private void AddExtraNumberFields()
    {
        if (textBoxes.Last().Text.Length > 1)
        {
            AddPhoneNumberField($"Phone Number {Fields.Count + 1}");
        }
    }

    private void DeleteExtraNumberFields()
    {
        for (int i = textBoxes.Count - 1; i > 0; i--)
        {
            if (textBoxes[i].Text.Length == 1)
            {
                if (textBoxes[i - 1].Text.Length == 1)
                {
                    RemoveFieldAndTextBoxAt(i);
                }
            }
        }
    }
}