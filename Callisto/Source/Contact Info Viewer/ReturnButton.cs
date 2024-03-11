using Callisto.ContactsListNode;

namespace Callisto.ContactInfoViewerNode;

class ReturnButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text = "<-";
        OnClick = GoToContactsList;
    }

    // Callback

    private void GoToContactsList()
    {
        ChangeScene(new ContactsList());
    }
}