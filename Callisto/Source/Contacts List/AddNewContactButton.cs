using Callisto.ContactEditorNode;
using Nodex;

namespace Callisto.ContactsListNode;

class AddNewContactButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "Add New Contact";
        Size = new(Window.Size.X, 40);
        actionOnClick = GoToContactEditor;
    }

    public override void Update()
    {
        base.Update();

        Size.X = Window.Size.X;
    }

    // Private

    private void GoToContactEditor()
    {
        ChangeScene(new ContactEditor());
    }
}