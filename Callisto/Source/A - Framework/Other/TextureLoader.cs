using SFML.Graphics;

namespace Nodex;

class TextureLoader
{
    // Fields

    public Texture Person;

    // Singleton

    private static TextureLoader? instance;

    public static TextureLoader Instance
    {
        get
        {
            instance ??= new();
            return instance;
        }
    }

    private TextureLoader()
    {
        Person = new("Resources/Person.jpg");
    }
}