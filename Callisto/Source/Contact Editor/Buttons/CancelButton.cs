using Callisto.ContactsListNode;
using Nodex;

namespace Callisto.ContactEditorNode.ButtonsNode;

class CancelButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "Cancel";
        TextColor = Color.Red;
        actionOnClick = CancelEdit;
    }

    public override void Update()
    {
        base.Update();

        Position.X = Window.Size.X - Size.X;
    }

    // Callback

    private void CancelEdit()
    {
        ChangeScene(new ContactsList());
    }
}