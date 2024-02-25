using Nodex;

namespace Callisto.NotificationDialogNode;

class NotificationDialog : Node
{
    // Fields

    public string Message = "";

    public override void Start()
    {
        base.Start();

        Program.MainWindow.RootNode.Deactivate();

        AddChild(new Label()
        {
            Message = Message
        });

        AddChild(new OkButton());
    }

    public override void Destroy()
    {
        base.Destroy();

        Program.MainWindow.RootNode.Activate();
        Program.RemoveWindow(Window);
    }
}