using SFML.Graphics;
using Callisto.ContactInfoViewerNode;
using Nodex;

namespace Callisto;

class ContactDisplayer : Node
{
    // Fields

    public string FullName = "";
    public int ContactIndex = 0;

    private Button button;

    // Public

    public override void Start()
    {
        CreateButton();
        CreateLabel();
    }

    public override void Update()
    {
        base.Update();
        button.Size.X = Window.Size.X;
    }

    // Private

    private void CreateButton()
    {
        button = new()
        {
            Size = new(250, 50),
            OutlineColor = Color.Transparent,
            ActionOnClick = GoToContactViewPage
        };

        AddChild(button);
    }

    private void CreateLabel()
    {
        AddChild(new Label()
        {
            Text     = FullName,
            Position = new(10, 12)
        });
    }

    // Callbacks

    private void GoToContactViewPage()
    {
        ChangeScene(new ContactInfoViewer()
        {
            ContactIndex = ContactIndex
        });
    }
}