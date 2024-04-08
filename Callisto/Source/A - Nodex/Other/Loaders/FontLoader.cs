namespace Callisto;

class FontLoader
{
    // Fields

    public Dictionary<string, Font> Fonts = [];

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
        Fonts.Add("RobotoMono", new("Resources/RobotoMono.ttf"));
    }
}