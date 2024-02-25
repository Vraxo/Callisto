using Nodex;
using SFML.Graphics;

namespace Callisto.DeletionDialogNode;

class CancelButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text          = "Cancel";
        Size          = new(100, 20);
        Position      = new(Window.Size.X - Size.X - 25, 75);
        FontSize      = 12;
        TextColor     = Color.Red;
        actionOnClick = Parent.Destroy;
    }
}