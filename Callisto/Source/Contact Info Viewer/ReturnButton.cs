using Nodex;

namespace Callisto.ContactInfoViewerNode;

class ReturnButton : Button
{
    // Public

    public override void Start()
    {
        base.Start();

        Text          = "<-";
        ActionOnClick = GoToContactsList;
    }

    // Callback

    private void GoToContactsList()
    {
        ChangeScene(new ContactsLoader());
    }
}