using Callisto.ContactInfoViewerNode;

namespace Callisto.ContactDisplayerNode;

class Button : Nodex.Button
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Size = new(Window.Size.X, 50);
        OutlineColor = Color.Transparent;
        actionOnClick = GoToContactViewPage;
    }

    public override void Update()
    {
        base.Update();

        Size.X = Window.Size.X;
    }

    // Callback

    private void GoToContactViewPage()
    {
        ChangeScene(new ContactInfoViewer()
        {
            ContactIndex = ContactIndex
        });
    }
}