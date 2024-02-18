using Nodex;

namespace Callisto.ContactEditorNode;

class ContactInfoField : Node
{
    // Fields

    public string LabelText;

    // Public

    public override void Start()
    {
        Label label = new()
        {
            Text = LabelText,
            Position = new(0, 0)
        };

        AddChild(label);

        TextBox textBox = new()
        {
            Position = new(0, 25)
        };

        AddChild(textBox);
    }
}