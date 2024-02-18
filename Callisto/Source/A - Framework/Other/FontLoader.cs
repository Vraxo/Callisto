using SFML.Graphics;

namespace Nodex;

class FontLoader
{
    // Fields

    public Font RobotoMono;

    // Singleton

    private static FontLoader? instance;

    public static FontLoader Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private FontLoader()
    {
        RobotoMono = new("Resources/RobotoMono.ttf");
    }
}