using Callisto.ContactEditorNode.ButtonsNode;
using Nodex;

namespace Callisto.ContactEditorNode;

class Scroller : VerticalViewScroller
{
    // AllFields

    private ContactEditor parent;

    // Public

    public override void Start()
    {
        base.Start();

        parent = GetParent<ContactEditor>();
    }

    public override void Update()
    {
        base.Update();

        float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
        CanGoUp = viewHeight > 0;

        //float contactsListHeight = 300 + parent.GetChild<Fields>().NumberFields.Count * 50;

        float maxContactEditorHeight = GetNode<OkButton>("Buttons/OkButton").Position.Y + 50;
        float maxYPosition = maxContactEditorHeight - Window.GetView().Size.Y;
        CanGoDown = viewHeight < maxYPosition;

        Console.WriteLine(maxYPosition);
        Console.WriteLine(CanGoDown);

        //float maxContactsListHeight = ContactsContainer.Instance.Contacts.Count * 50;
        //float maxYPosition = maxContactsListHeight - Window.GetView().Size.Y;
        //CanGoDown = viewHeight < maxYPosition;
    }
}