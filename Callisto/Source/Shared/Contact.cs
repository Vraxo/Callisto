namespace Callisto;

class Contact
{
    // Properties

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<string> PhoneNumbers { get; set; }

    // Public

    public string GetFullName()
    {
        return FirstName + " " + LastName;
    }
}
