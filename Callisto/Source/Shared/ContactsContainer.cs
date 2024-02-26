using System.Diagnostics;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Callisto;

class ContactsContainer
{
    // Fields

    public List<Contact> Contacts = [];

    // Singleton

    private static ContactsContainer instance;

    public static ContactsContainer Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private ContactsContainer() { }

    // Public

    public void Add(Contact contact)
    {
        Contacts.Add(contact);
        Save();
    }

    public void Delete(int index)
    {
        Contacts.RemoveAt(index);
        Save();
        Load();
    }

    public void Load()
    {
        var filePath = "Resources/Contacts.yaml";

        if (File.Exists(filePath))
        {
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance) // Use CamelCaseNamingConvention
                .Build();

            using var reader = new StreamReader(filePath);
            var yamlContacts = deserializer.Deserialize<List<Contact>>(reader);
            Contacts = yamlContacts ?? [];
        }
    }

    public void Save()
    {
        Contacts = Contacts.OrderBy(o => o.FirstName).ToList();

        var filePath = "Resources/Contacts.yaml";

        var serializer = new SerializerBuilder()
            .WithNamingConvention(PascalCaseNamingConvention.Instance) // Use CamelCaseNamingConvention
            .Build();

        var yaml = serializer.Serialize(Contacts);

        File.WriteAllText(filePath, yaml);
    }

    public bool ContactExists(Contact newContact)
    {
        foreach (Contact contact in Contacts)
        {
            bool sameFirstName = contact.FirstName == newContact.FirstName;
            bool sameLastname = contact.LastName == newContact.LastName;

            if (sameFirstName && sameLastname)
            {
                return true;
            }
        }

        return false;
    }
}