using Nodex;

namespace Callisto.ContactInfoViewerNode;

class CopyNumberButtons : Node
{
    // AllFields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];

        Console.WriteLine(contact.PhoneNumbers.Count);

        for (int i = 0; i < contact.PhoneNumbers.Count; i++)
        {
            Button numberButton = new CopyNumberButton()
            {
                ContactIndex = ContactIndex,
                NumberIndex = i,
                Text = contact.PhoneNumbers[i],
            };

            AddChild(numberButton);

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
}