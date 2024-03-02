namespace Callisto.ContactDisplayerNode;

class Label : Nodex.Label
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Text = ContactsContainer.Instance.Contacts[ContactIndex].GetFullName();
        Position = new(60, 12);
    }
}