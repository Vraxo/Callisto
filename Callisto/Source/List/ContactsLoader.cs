using Callisto.ContactEditorNode;
using Nodex;

namespace Callisto;

class ContactsLoader : Node
{
    // Fields

    private Button newContactButton;
    private Nodex.Scroller scroller;

    // Public

    public override void Start()
    {
        CreateNewContactButton();
        CreateContactDisplayers();
        CreateScroller();
    }

    public override void Update()
    {
        base.Update();

        UpdateNewContactButton();
        UpdateScroller();
    }

    // Create nodes

    private void CreateNewContactButton()
    {
        newContactButton = new()
        {
            Text = "Add New Contact",
            Size = new(100, 35),
            
            ActionOnClick = () =>
            {
                ChangeScene(new ContactEditor());
            }
        };

        AddChild(newContactButton);
    }

    private void CreateContactDisplayers()
    {
        ContactsContainer.Instance.Load();

        List<Contact> contacts = ContactsContainer.Instance.Contacts;

        for (int i = 0; i < contacts.Count; i++)
        {
            ContactDisplayer contactDisplayer = new()
            {
                ContactIndex = i,
                FullName = contacts[i].GetFullName(),
                Position = new(0, 35 + 50 *  i)
            };

            AddChild(contactDisplayer, $"ContactDisplayer{i + 1}");
        }
    }

    private void CreateScroller()
    {
        scroller = new();
        AddChild(scroller);
    }

    // Update nodes

    private void UpdateNewContactButton()
    {
        newContactButton.Size.X = Window.Size.X;
    }

    private void UpdateScroller()
    {
        float viewHeight = Window.GetView().Center.Y - Window.GetView().Size.Y / 2;
        scroller.CanGoUp = viewHeight > 0;
        
        float contactsListHeight = (ContactsContainer.Instance.Contacts.Count - 10) * 50;
        scroller.CanGoDown = viewHeight < contactsListHeight;
    }
}