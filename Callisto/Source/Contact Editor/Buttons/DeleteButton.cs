using Callisto.DeletionDialogNode;
using Nodex;
using SFML.Graphics;
using SFML.Window;
using Window = Nodex.Window;

namespace Callisto.ContactEditorNode.ButtonsNode;

class DeleteButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "Delete";
        TextColor = Color.Red;
        actionOnClick = GetConfirmationForContactDeletion;
    }

    public override void Update()
    {
        base.Update();

        Position.X = Window.Size.X - Size.X;
    }

    // Callback

    private void GetConfirmationForContactDeletion()
    {
        Window window = new
        (
            new(360, 120),
            "Confirm contact deletion",
            Styles.Close,
            new ContextSettings(0, 0, 16)
        )
        {
            RootNode = new DeletionDialog()
        };

        window.Start();
        Program.AddWindow(window);
    }
}