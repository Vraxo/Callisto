using Callisto;
using SFML.Window;
using Callisto.ContactsListNode;
using Callisto.DeletionDialogNode;

namespace Callisto.ContactEditorNode.ButtonsNode;

class Buttons : Node
{
    // AllFields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        AddOkButton();
        AddDeleteButton();
        AddCancelButton();
    }

    // Create nodes

    private void AddOkButton()
    {
        AddChild(new OkButton()
        {
            ContactIndex = ContactIndex
        });
    }

    private void AddDeleteButton()
    {
        if (ContactIndex == -1) return;

        AddChild(new Button
        {
            Text = "Delete",
            Style = new()
            {
                TextColor = Color.Red
            },
            OnClick = GetConfirmationForContactDeletion,
            OnUpdate = (button) =>
            {
                button.Position.X = Window.Size.X - button.Size.X;
            }
        });
    }

    private void AddCancelButton()
    {
        AddChild(new Button
        {
            Text = "Cancel",
            Style = new()
            {
                TextColor = Color.Red
            },
            OnUpdate = (button) =>
            {
                if (ContactIndex == -1)
                {
                    button.Position.X = Window.Size.X - button.Size.X;
                }
                else
                {
                    button.Position.X = 0;
                }
            },
            OnClick = () =>
            {
                ChangeScene(new ContactsList());
            }
        });
    }

    // Callbacks

    private void GetConfirmationForContactDeletion()
    {
        WindowInfo windowInfo = new()
        {
            VideoMode = new(360, 120),
            Title = "Confirm contact deletion",
            Styles = Styles.Close,
            ContextSettings = new(0, 0, 16),
            ClearColor = new(32, 32, 32)
        };

        Window window = new(windowInfo)
        {
            RootNode = new DeletionDialog()
        };

        window.Start();
        Program.AddWindow(window);
    }
}