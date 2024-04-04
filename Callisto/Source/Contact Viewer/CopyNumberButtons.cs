using Nodex;
using System.Text.RegularExpressions;

namespace Callisto.ContactInfoViewerNode;

class CopyNumberButtons : Node
{
    // Fields

    public int ContactIndex = -1;
    public List<CopyNumberButton> Buttons = [];

    // Public

    public override void Start()
    {
        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];

        Console.WriteLine(contact.PhoneNumbers.Count);

        for (int i = 0; i < contact.PhoneNumbers.Count; i++)
        {
            CopyNumberButton numberButton = new()
            {
                ContactIndex = ContactIndex,
                NumberIndex = i,
                Text = FormatPhoneNumber(contact.PhoneNumbers[i]),
            };

            AddChild(numberButton);
            Buttons.Add(numberButton);

            numberButton.Origin = numberButton.Size / 2;
            numberButton.TextOrigin = numberButton.Origin;
        }
    }

    public override void Update()
    {
        base.Update();

        for (int i = 0; i < Children.Count; i++)
        {
            Children[i].Position.X = (Window.Size.X / 2);
            Children[i].Position.Y = (Window.Size.Y * 0.55F) + (50 * i);
        }
    }

    // Private

    private string FormatPhoneNumber(string phoneNumber)
    {
        if (phoneNumber.Length <= 11) return phoneNumber;

        phoneNumber = phoneNumber.Substring(1);

        int countryCodeLength = phoneNumber.Length - 10;

        string formattedNumber = string.Format("+{0} {1} {2} {3}",
                                    phoneNumber.Substring(0, countryCodeLength),
                                    phoneNumber.Substring(countryCodeLength, 3),
                                    phoneNumber.Substring(countryCodeLength + 3, 3),
                                    phoneNumber.Substring(countryCodeLength + 6));

        return formattedNumber;
    }
}