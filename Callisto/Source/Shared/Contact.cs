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
}