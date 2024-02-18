using Nodex;

namespace Callisto.DeletionDialogNode;

class DeletionDialog : Node
{
    // Fields
    
    private Nodex.Label label;
    private Button confirmButton;
    private Button cancelButton;

    // Public

    public override void Start()
    {
        Program.MainWindow.RootNode.Deactivate();
        
        CreateLabel();
        CreateConfirmButton();
        CreateCancelButton();
    }

    public override void Destroy()
    {
        base.Destroy();

        Program.MainWindow.RootNode.Activate();
        Program.RemoveWindow(Window);
    }

    // Create nodes

    private void CreateLabel()
    {
        label = new Label();
        AddChild(label);
    }

    private void CreateConfirmButton()
    {
        confirmButton = new ConfirmButton();
        AddChild(confirmButton);
    }

    private void CreateCancelButton()
    {
        cancelButton = new CancelButton();
        AddChild(cancelButton);
    }
}