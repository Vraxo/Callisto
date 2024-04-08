using Callisto;
using Callisto.ContactDisplayerNode;
using Callisto.ContactEditorNode;

namespace Callisto.ContactsListNode;

class ContactsList : Node
{
    // Fields

    private Button addNewContactButton;

    // Public

    public override void Start()
    {
        new Initializer().Initialize();

        AddAddNewContactButton();
        AddContactDisplayers();
        AddScroller();
    }

    // Private

    private void AddContactDisplayers()
    {
        List<Contact> contacts = ContactsContainer.Instance.Contacts;

        for (int i = 0; i < contacts.Count; i++)
        {
            ContactDisplayer contactDisplayer = new()
            {
                ContactIndex = i
            };

            AddChild(contactDisplayer);

            var button = contactDisplayer.GetChild<Button>();

            float x = 0;
            float y = addNewContactButton.Size.Y + button.Size.Y * i;

            contactDisplayer.Position = new(x, y);
        }
    }

    private void AddAddNewContactButton()
    {
        addNewContactButton = new()
        {
            Text = "Add New Contact",
            Size = new(Window.Size.X, 40),
            OnUpdate = (button) =>
            {
                button.Size.X = Window.Size.X;
            },
            OnClick = () => ChangeScene(new ContactEditor())
        };

        AddChild(addNewContactButton);
    }

    private void AddScroller()
    {
        AddChild(new VerticalViewScroller
        {
            OnUpdate = (scroller) =>
            {
                float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
                float maxContactsListHeight = ContactsContainer.Instance.Contacts.Count * 50;
                float maxYPosition = maxContactsListHeight - Window.GetView().Size.Y;
                scroller.CanGoDown = viewHeight < maxYPosition;
            }
        });
    }
}