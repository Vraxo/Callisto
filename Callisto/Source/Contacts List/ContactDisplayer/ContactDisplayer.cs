using Nodex;
using Callisto.ContactInfoViewerNode;

namespace Callisto.ContactDisplayerNode;

class ContactDisplayer : Node
{
    // Fields

    public int ContactIndex = 0;

    // Public

    public override void Start()
    {
        AddChild(new Button
        {
            Size = new(Window.Size.X, 50),
            Style = new()
            {
                OutlineColor = Color.Transparent
            },
            OnUpdate = (button) =>
            {
                button.Size.X = Window.Size.X;
            },
            OnClick = () =>
            {
                ChangeScene(new ContactViewer()
                {
                    ContactIndex = ContactIndex
                });
            }
        });

        AddChild(new CircleSprite()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new NameLabel
        {
            ContactIndex = ContactIndex
        });
    }
}