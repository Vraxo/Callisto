using Callisto;

namespace Callisto;

class AvatarLoader
{
    // Singleton

    private static AvatarLoader instance;

    public static AvatarLoader Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private AvatarLoader() { }

    // Public

    public void Load()
    {
        foreach (Contact contact in ContactsContainer.Instance.Contacts)
        {
            string id = contact.Id.ToString();

            if (!contact.HasAvatar || TextureLoader.Instance.Textures.ContainsKey(id))
            {
                continue;
            }
            
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

        ContactsContainer.Instance.Reload();
    }

    // Private

    private string GetTextureExtension(string imageName)
    {
        string extension = File.Exists($"{imageName}.jpg") ?
                           ".jpg" :
                           ".png";

        return extension;
    }
}