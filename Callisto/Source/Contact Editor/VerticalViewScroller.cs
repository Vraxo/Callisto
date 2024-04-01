namespace Callisto.ContactEditorNode;

class VerticalViewScroller : Nodex.VerticalViewScroller
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

        float contactsListHeight = 300 + parent.GetChild<Fields>().NumberFields.Count * 50;
        CanGoDown = viewHeight < contactsListHeight;
    }
}