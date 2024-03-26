using Nodex;

namespace Callisto.ContactEditorNode;

class ContactInfoField : Node
{
    // Fields

    public string LabelText;

    // Public

    public override void Start()
    {
        AddChild(new Label
        {
            Text = LabelText,
            Position = new(0, 0)
        });

        AddChild(new TextBox
        {
            Position = new(0, 25),
            Size = new(300, 50)
        });
    }
}