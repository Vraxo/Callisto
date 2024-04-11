namespace Callisto;

class ButtonStyle
{
    public float OutlineThickness = 0;

    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.Fonts["RobotoMono"];

    public Color TextColor = Color.White;
    public Color OutlineColor = Color.Black;
    public Color FillColor = new(64, 64, 64);
    public Color HoverFillColor = new(96, 96, 96);
    public Color UnpressedFillColor = new(64, 64, 64);
    public Color PressedFillColor = new(48, 48, 48);
}