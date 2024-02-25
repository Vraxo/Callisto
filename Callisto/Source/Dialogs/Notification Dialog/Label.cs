namespace Callisto.NotificationDialogNode;

class Label : Nodex.Label
{
    // Fields

    public string Message = "";

    // Public

    public override void Start()
    {
        Position = new(25, 15);
        Text     = Message;
        FontSize = 14;
    }
}