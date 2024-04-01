using Callisto.ContactEditorNode.ButtonsNode;
using Nodex;

namespace Callisto.ContactEditorNode;

class Scroller : VerticalViewScroller
{
    // Fields

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

        float maxContactEditorHeight = GetNode<OkButton>("Buttons/OkButton").Position.Y + 50;
        float maxYPosition = maxContactEditorHeight - Window.GetView().Size.Y;
        CanGoDown = viewHeight < maxYPosition;
    }
}