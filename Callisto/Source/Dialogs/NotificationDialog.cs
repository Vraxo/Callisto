using Nodex;

namespace Callisto;

class NotificationDialog : Node
{
    // AllFields

    public string Message = "";

    public override void Start()
    {
        base.Start();

        Program.MainWindow.RootNode.Deactivate();

        AddChild(new Label()
        {
            Text = Message,
            Position = new(25, 15),
            FontSize = 14
        });

        AddChild(new Button()
        {
            Text = "OK",
            Position = new(25, 75),
            Size = new(100, 20),
            Style = new()
            {
                FontSize = 12,
                TextColor = Color.Green,
            },
            OnClick = Destroy,
        });
    }

    public override void Destroy()
    {
        base.Destroy();
        Program.MainWindow.RootNode.Activate();
        Program.RemoveWindow(Window);
    }
}