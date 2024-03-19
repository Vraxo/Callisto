using Nodex;
using Callisto.ContactsListNode;

namespace Callisto.ContactEditorNode.ButtonsNode;

class Buttons : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        CreateOkButton();
        CreateDeleteButton();
        CreateCancelButton();
    }

    // Create nodes

    private void CreateOkButton()
    {
        AddChild(new OkButton
        {
            ContactIndex = ContactIndex
        });
    }

    private void CreateDeleteButton()
    {
        if (ContactIndex == -1) return;

        AddChild(new DeleteButton());
    }

    private void CreateCancelButton()
    {
        if (ContactIndex != -1) return;

        AddChild(new Button
        {
            Text = "Cancel",
            Style = new()
            {
                TextColor = Color.Red
            },
            OnUpdate = (button) =>
            {
                button.Position.X = Window.Size.X - button.Size.X;
            },
            OnClick = () =>
            {
                ChangeScene(new ContactsList());
            }
        });
    }
}