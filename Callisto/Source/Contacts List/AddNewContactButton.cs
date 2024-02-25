using Nodex;
using Callisto.ContactEditorNode;

namespace Callisto.ContactsListNode;

class AddNewContactButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text          = "Add New Contact";
        Size          = new(100, 40);
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