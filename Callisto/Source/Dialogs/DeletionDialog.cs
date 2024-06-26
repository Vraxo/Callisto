﻿using Callisto;
using Callisto.ContactEditorNode;

namespace Callisto.DeletionDialogNode;

class DeletionDialog : Node
{
    // Public

    public override void Start()
    {
        Program.MainWindow.RootNode.Deactivate();

        AddLabel();
        AddConfirmButton();
        AddCancelButton();
    }

    public override void Destroy()
    {
        base.Destroy();

        Program.MainWindow.RootNode.Activate();
        Program.RemoveWindow(Window);
    }

    // Private

    private void AddLabel()
    {
        AddChild(new Label
        {
            Position = new(25, 15),
            Text = "Are you sure you want to delete\nthis contact?",
            FontSize = 14
        });
    }

    private void AddConfirmButton()
    {
        AddChild(new Button
        {
            Text = "Confirm",
            Position = new(25, 75),
            Size = new(100, 20),
            Style = new()
            {
                FontSize = 12,
                TextColor = Color.Green
            },
            OnClick = () =>
            {
                Destroy();
                Program.MainWindow.RootNode.GetNode<ContactEditor>("").DeleteContact();
            }
        });
    }

    private void AddCancelButton()
    {
        AddChild(new Button
        {
            Text = "Cancel",
            Size = new(100, 20),
            Position = new(Window.Size.X - 100 - 25, 75),
            Style = new()
            {
                FontSize = 12,
                TextColor = Color.Red,
            },
            OnClick = Destroy,
        });
    }
}