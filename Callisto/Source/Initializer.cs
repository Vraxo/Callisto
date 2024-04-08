using Callisto;
using Callisto.Properties;

namespace Callisto;

class Initializer
{
    // Public

    public void Initialize()
    {
        AddResourcesDirectory();
        AddDefaultFont();
        AddDefaultAvatar();
        AddIcon();
        LoadContacts();
    }

    // Private

    private void LoadContacts()
    {
        ContactsContainer.Instance.Load();
        AvatarLoader.Instance.Load();
    }

    private void AddResourcesDirectory()
    {
        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }
    }

    private void AddDefaultFont()
    {
        string defaultFontPath = "Resources/RobotoMono.ttf";

        if (!File.Exists(defaultFontPath))
        {
            File.WriteAllBytes(defaultFontPath, Resources.RobotoMono);
        }
    }

    private void AddDefaultAvatar()
    {
        string defaultAvatarPath = "Resources/DefaultAvatar.jpg";

        if (!File.Exists(defaultAvatarPath))
        {
            Resources.DefaultAvatar.Save(defaultAvatarPath);
        }
    }

    private void AddIcon()
    {
        string iconPath = "Resources/Icon.jpg";

        if (!File.Exists(iconPath))
        {
            Resources.Icon.Save(iconPath);
        }

        Program.MainWindow.SetIcon(256, 256, new Image(iconPath).Pixels);
    }
}