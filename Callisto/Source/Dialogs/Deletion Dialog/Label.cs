namespace Callisto.DeletionDialogNode;

class Label : Nodex.Label
{
    // Public

    public override void Start()
    {
        Position = new(25, 15);
        Text = "Are you sure you want to delete\nthis contact?";
        FontSize = 14;
    }
}