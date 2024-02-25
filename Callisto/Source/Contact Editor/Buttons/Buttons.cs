using Nodex;

namespace Callisto.ContactEditorNode.ButtonsNode;

class Buttons : Node
{
    // Fields

    public int ContactIndex = -1;

    private Button okButton;
    private Button deleteButton;
    private Button cancelButton;

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
        okButton = new OkButton()
        {
            ContactIndex = ContactIndex
        };

        AddChild(okButton);
    }

    private void CreateDeleteButton()
    {
        if (ContactIndex == -1) return;

        deleteButton = new DeleteButton();
        AddChild(deleteButton, "DeleteButton");
    }

    private void CreateCancelButton()
    {
        if (ContactIndex != -1) return;

        cancelButton = new CancelButton();
        AddChild(cancelButton, "CancelButton");
    }
}