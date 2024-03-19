namespace Callisto;

class NameLabel : Label
{
    public int ContactIndex = 0;

    public override void Start()
    {
        base.Start();

        Position = new(60, 12);
    }

    public override void Update()
    {
        base.Update();

        Text = ContactsContainer.Instance.Contacts[ContactIndex].GetFullName();

        //string s = "ABCDEFGH";
        //s = s.Remove(3, 2).Insert(3, "ZX");
        //

        int i = 0;

        //Text = Text.Remove(Text.Length, 0);

        while (Renderer.GetLocalBounds().Width > Window.Size.X)
        {
            Text = Text.Substring(0, Text.Length - 1 - i);
            Renderer.DisplayedString = Text;
            i ++;
        }
    }
}