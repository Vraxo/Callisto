namespace Callisto.NotificationDialogNode;

class OkButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "OK";
        Position = new(25, 75);
        Size = new(100, 20);
        FontSize = 12;
        Style.TextColor = Color.Green;
        actionOnClick = Parent.Destroy;
    }
}