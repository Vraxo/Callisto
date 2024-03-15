using Nodex;
using Callisto.AvatarDisplayerNode;
using Callisto.ContactsListNode;
using Callisto.ContactEditorNode;

namespace Callisto.ContactInfoViewerNode;

class ContactInfoViewer : Node
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
        }) ;

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

        AddChild(new Nodex.VerticalViewScroller
        {
            OnUpdate = (scroller) =>
            {
                float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
                scroller.CanGoUp = viewHeight > 0;

                Contact contact = ContactsContainer.Instance.Contacts[ContactIndex];
                
                float contactsListHeight = contact.PhoneNumbers.Count * 50;
                scroller.CanGoDown = viewHeight < contactsListHeight;
            }
        });
    }
}