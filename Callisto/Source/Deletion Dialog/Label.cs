using SFML.System;

namespace Callisto.DeletionDialogNode;

class Label : Nodex.Label
{
    // Fields

    private readonly Vector2f position = new(25, 15);
    private readonly string message = "Are you sure you want to delete\nthis contact?";
    private readonly uint fontSize = 14;

    // Public

    public override void Start()
    {
        Position = position;
        Text     = message;
        FontSize = fontSize;
    }
}