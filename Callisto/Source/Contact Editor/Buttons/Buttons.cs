using Nodex;
using Callisto.ContactsListNode;

namespace Callisto.ContactEditorNode.ButtonsNode;

class Buttons : Node
{
    // AllFields

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
        AddChild(new Button
        {
            Text = "Cancel",
            Style = new()
            {
                TextColor = Color.Red
            },
            OnUpdate = (button) =>
            {
                if (ContactIndex == -1)
                {
                    button.Position.X = Window.Size.X - button.Size.X;
                }
                else
                {
                    button.Position.X = 0;
                }
            },
            OnClick = () =>
            {
                ChangeScene(new ContactsList());
            }
        });
    }
}