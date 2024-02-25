using Nodex;

namespace Callisto.ContactDisplayerNode;

class ContactDisplayer : Node
{
    // Fields

    public int ContactIndex = 0;

    // Public

    public override void Start()
    {
        AddChild(new Button()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new CircleSprite()
        {
            ContactIndex = ContactIndex
        });

        AddChild(new Label()
        {
            ContactIndex = ContactIndex
        });
    }
}