using Nodex;
using SFML.Window;
using Callisto.DeletionDialogNode;
using Window = Nodex.Window;

namespace Callisto.ContactEditorNode.ButtonsNode;

class DeleteButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "Delete";
        Style.TextColor = Color.Red;
        OnClick = GetConfirmationForContactDeletion;
    }

    public override void Update()
    {
        base.Update();

        Position.X = Window.Size.X - Size.X;
    }

    // Callback

    private void GetConfirmationForContactDeletion()
    {
        WindowInfo windowInfo = new()
        {
            VideoMode = new(360, 120),
            Title = "Confirm contact deletion",
            Styles = Styles.Close,
            ContextSettings = new(0, 0, 16)
        };

        Window window = new(windowInfo)
        {
            RootNode = new DeletionDialog()
        };

        window.Start();
        Program.AddWindow(window);
    }
}