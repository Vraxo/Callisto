namespace Callisto;

class TextBoxStyle
{
    public int OutlineThickness = -1;

    public Font Font = FontLoader.Instance.Fonts["RobotoMono"];
    public uint FontSize = 16;

    public float Padding = 8;

    public Color TextColor = new(255, 255, 255);
    public Color SelectedOutlineColor = new(32, 32, 255);
    public Color DeselectedOutlineColor = new(0, 0, 0, 0);
    public Color FillColor = new(16, 16, 16);
    public Color IdleFillColor = new(16, 16, 16);
    public Color HoverFillColor = new(16, 16, 16);
}