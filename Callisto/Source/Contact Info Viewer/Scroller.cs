namespace Callisto.ContactInfoViewerNode;

class Scroller : Nodex.Scroller
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
        CanGoUp = viewHeight > 0;

        float contactsListHeight = ContactsContainer.Instance.Contacts[ContactIndex].PhoneNumbers.Count * 50;
        CanGoDown = viewHeight < contactsListHeight;
    }
}