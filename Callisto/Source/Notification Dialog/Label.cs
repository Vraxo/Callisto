using SFML.System;

namespace Callisto.NotificationDialogNode;

class Label : Nodex.Label
{
    // Fields

    public string Message = "";
    private readonly Vector2f position = new(25, 15);
    private readonly uint fontSize = 14;

    // Public

    public override void Start()
    {
        Position = position;
        Text     = Message;
        FontSize = fontSize;
    }
}