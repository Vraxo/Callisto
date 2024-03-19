namespace Callisto;

class NameLabel : Label
{
    // Fields

    public int ContactIndex = 0;

    private string fullName;

    // Public

    public override void Start()
    {
        base.Start();

        Position = new(60, 12);
        fullName = ContactsContainer.Instance.Contacts[ContactIndex].GetFullName();
    }

    public override void Update()
    {
        base.Update();

        Text = GetDottedName();
    }
    
    // Private

    private string GetShortenedName()
    {
        string shortenedName = fullName;
        Renderer.DisplayedString = fullName;

        int i = 0;

        while (Renderer.GetLocalBounds().Width > Window.Size.X - 60)
        {
            shortenedName = shortenedName.Substring(0, fullName.Length - i);
            Renderer.DisplayedString = shortenedName;
            i ++;
        }

        return shortenedName;
    }

    private string GetDottedName()
    {
        string dottedName = GetShortenedName();

        if (dottedName != fullName)
        {
            for (int i = 0; i < 3; i++)
            {
                dottedName = dottedName.Remove(dottedName.Length - 3 + i, 1).Insert(dottedName.Length - 3 + i, ".");
            }
        }

        return dottedName;
    }
}