using Nodex;
using Callisto.Properties;

namespace Callisto;

class Initializer
{
    public void Initialize()
    {
        CreateResourcesDirectory();
        CreateDefaultFont();
        CreateDefaultAvatar();
        CreateIcon();
    }

    private void CreateResourcesDirectory()
    {
        if (!Directory.Exists("Resources"))
        {
            Directory.CreateDirectory("Resources");
        }
    }

    private void CreateDefaultFont()
    {
        string defaultFontPath = "Resources/RobotoMono.ttf";

        if (!File.Exists(defaultFontPath))
        {
            File.WriteAllBytes(defaultFontPath, Resources.RobotoMono);
        }
    }

    private void CreateDefaultAvatar()
    {
        string defaultAvatarPath = "Resources/DefaultAvatar.jpg";

        if (!File.Exists(defaultAvatarPath))
        {
            Resources.DefaultAvatar.Save(defaultAvatarPath);
        }
    }

    private void CreateIcon()
    {
        string iconPath = "Resources/Icon.jpg";

        if (!File.Exists(iconPath))
        {
            Resources.Icon.Save(iconPath);
        }

        Program.MainWindow.SetIcon(256, 256, new Image(iconPath).Pixels);
    }
}