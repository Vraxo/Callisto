using Callisto;
using System.Text.Json;
using System.Text.RegularExpressions;

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

    public static string PadNumbers(string input)
    {
        return Regex.Replace(input, "[0-9]+", match => match.Value.PadLeft(10, '0'));
    }

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

    public void Reload()
    {
        Save();
        Load();
    }

    public void Save()
    {
        Contacts = Contacts.OrderBy(o => PadNumbers(o.FirstName)).ToList();

        string filePath = "Resources/Contacts.json";

        JsonSerializerOptions options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(Contacts, options);

        File.WriteAllText(filePath, json);
    }

    public void Load()
    {
        string filePath = "Resources/Contacts.json";

        if (File.Exists(filePath))
        {
            JsonSerializerOptions options = new()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            using FileStream stream = File.OpenRead(filePath);
            List<Contact> jsonContacts = JsonSerializer.Deserialize<List<Contact>>(stream, options);
            Contacts = jsonContacts ?? new List<Contact>();
        }
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

    // Private

    private void LoadAvatarTextures()
    {
        foreach (Contact contact in Contacts)
        {
            string id = contact.Id.ToString();

            if (!contact.HasAvatar || TextureLoader.Instance.Textures.ContainsKey(id))
            {
                continue;
            }

            if (!TextureLoader.Instance.Textures.ContainsKey(id))
            {
                string extension = GetTextureExtension($"Resources/Avatars/{contact.Id}");
                string path = $"Resources/Avatars/{contact.Id}{extension}";

                if (File.Exists(path))
                {
                    try
                    {
                        TextureLoader.Instance.Textures.Add(id, new(path));
                    }
                    catch
                    {
                        contact.HasAvatar = false;
                    }
                }
                else
                {
                    contact.HasAvatar = false;
                }
            }
        }
        
        Save();
        Load();
    }

    private string GetTextureExtension(string imageName)
    {
        string extension = File.Exists($"{imageName}.jpg") ? 
                           ".jpg" : 
                           ".png";

        return extension;
    }
}