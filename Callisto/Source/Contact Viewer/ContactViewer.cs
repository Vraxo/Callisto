using Nodex;
using Callisto.ContactsListNode;
using Callisto.ContactEditorNode;
using Callisto.ContactEditorNode.ButtonsNode;

namespace Callisto.ContactInfoViewerNode;

class ContactViewer : Node
{
    // Fields

    public int ContactIndex = -1;

    // Public

    public override void Start()
    {
        AddChild(new AvatarDisplayer
        {
            ContactIndex = ContactIndex
        });

        AddChild(new Button
        {
            Text = "<-",
            OnClick = () =>
            {
                ChangeScene(new ContactsList());
            }
        });

        AddChild(new Button
        {
            Text = "Edit",
            OnUpdate = (button) =>
            {
                button.Position.X = button.Window.Size.X - button.Size.X;
            },
            OnClick = () =>
            {
                ChangeScene(new ContactEditor
                {
                    ContactIndex = ContactIndex
                });
            }
        });

        AddChild(new NameLabel
        {
            ContactIndex = ContactIndex
        });

        AddChild(new CopyNumberButtons
        {
            ContactIndex = ContactIndex
        });

        AddScroller();
    }

    // Private

    private void AddScroller()
    {
        AddChild(new VerticalViewScroller
        {
            OnUpdate = (scroller) =>
            {
                float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;

                Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];

                var copyNumberButtons = GetNode<CopyNumberButtons>("CopyNumberButtons");

                float maxContactEditorHeight;

                if (copyNumberButtons.Buttons.Count > 0)
                {
                    maxContactEditorHeight = copyNumberButtons.Buttons.Last().Position.Y + 50;
                }
                else
                {
                    maxContactEditorHeight = 0;
                }

                float maxYPosition = maxContactEditorHeight - Window.GetView().Size.Y;
                scroller.CanGoDown = viewHeight < maxYPosition;
            }
        });
    }
}