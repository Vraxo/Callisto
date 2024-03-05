namespace Callisto;

class Contact
{
    // Properties

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PhoneNumbers { get; set; }
    public bool HasAvatar { get; set; }

    // Public

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }

    // Static

    public static int GenerateUniqueId()
    {
        List<int> ids = [];

        foreach (Contact contact in ContactsContainer.Instance.Contacts)
        {
            ids.Add(contact.Id);
        }

        Random random = new();

        int id = random.Next();

        while (ids.Contains(id))
        {
            id = random.Next();
        }

        return id;
    }
}