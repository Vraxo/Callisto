namespace Callisto;

class Contact
{
    // Properties

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PhoneNumbers { get; set; }
    public bool HasAvatar { get; set; }

    // Constructor

    public Contact()
    {
        HasAvatar = false;

        Random random = new();
        Id = random.Next();
    }

    // Public

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}