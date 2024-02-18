using Callisto.ContactEditorNode;
using Nodex;

namespace Callisto.ContactInfoViewerNode;

class EditButton : Button
{
    // Field

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        base.Start();

        Text          = "Edit";
        ActionOnClick = GoToContactEditor;
    }

    public override void Update()
    {
        base.Update();

        Position.X = Window.Size.X - Size.X;
    }

    // Callback

    private void GoToContactEditor()
    {
        ChangeScene(new ContactEditor()
        {
            ContactIndex = ContactIndex
        });
    }
}