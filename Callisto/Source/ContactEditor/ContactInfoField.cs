using Callisto;

namespace Callisto.ContactEditorNode;

class ContactInfoField : Node
{
    // AllFields

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
        });
    }
}