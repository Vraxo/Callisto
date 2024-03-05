using Callisto.ContactEditorNode;
using Nodex;

namespace Callisto.DeletionDialogNode;

class ConfirmButton : Button
{
    // Fields

    private ContactEditor contactEditor;

    // Public

    public override void Start()
    {
        base.Start();

        contactEditor = Program.MainWindow.RootNode.GetNode<ContactEditor>("");

        Text = "Confirm";
        Position = new(25, 75);
        Size = new(100, 20);
        FontSize = 12;
        TextColor = Color.Green;
        actionOnClick = Confirm;
    }

    // Callback

    private void Confirm()
    {
        Parent.Destroy();
        contactEditor.DeleteContact();
    }
}