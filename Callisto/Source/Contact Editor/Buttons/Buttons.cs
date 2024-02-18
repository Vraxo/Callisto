using Nodex;

namespace Callisto.ContactEditorNode.ButtonsNode;

class Buttons : Node
{
    // Fields

    public int Index = -1;

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
        okButton = new OkButton();
        AddChild(okButton);
    }

    private void CreateDeleteButton()
    {
        if (Index == -1) return;

        deleteButton = new DeleteButton();
        AddChild(deleteButton, "DeleteButton");
    }

    private void CreateCancelButton()
    {
        if (Index != -1) return;

        cancelButton = new CancelButton();
        AddChild(cancelButton, "CancelButton");
    }
}