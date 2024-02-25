namespace Callisto.ContactsListNode;

class Scroller : Nodex.Scroller
{
    // Public

    public override void Update()
    {
        base.Update();

        float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
        CanGoUp = viewHeight > 0;

        float maxContactsListHeight = ContactsContainer.Instance.Contacts.Count * 50;
        float maxYPosition = maxContactsListHeight - Window.GetView().Size.Y;
        CanGoDown = viewHeight < maxYPosition;
    }
}