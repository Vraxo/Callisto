using SFML.Graphics;

namespace Callisto;

class Caret : Node
{
    // Fields

    public int MaxTime = 2000;

    private const int MinTime = 0;
    private const byte MinAlpha = 0;
    private const byte MaxAlpha = 255;

    private int timer = 0;
    private byte alpha = 255;

    private TextBox parent;
    private Text renderer = new();

    // Properties

    private int x = 0;

    public int X
    {
        get
        {
            return x;
        }

        set
        {
            if (value > x)
            {
                if (x < parent.Text.Length)
                {
                    x = value;
                }
            }
            else
            {
                if (x > 0)
                {
                    x = value;
                }
            }

            alpha = MaxAlpha;
        }
    }

    // Public

    public override void Start()
    {
        base.Start();

        parent = GetParent<TextBox>();
    }

    public override void Update()
    {
        base.Update();

        if (!parent.Selected) return;

        Draw();
        UpdateAlpha();
    }

    public void GetInPosition(float mouseX)
    {
        if (parent.Text.Length == 0)
        {
            X = 0;
        }
        else
        {
            int x = (int)(mouseX - parent.GlobalPosition.X - parent.Style.Padding);
            int characterWidth = (int)(parent.TextRenderer.GetGlobalBounds().Width / parent.Text.Length);
            X = x / characterWidth;

            X = X > parent.Text.Length ?
                parent.Text.Length :
                X;
        }
    }

    // Private

    private void Draw()
    {
        float characterWidth = (parent.Style.FontSize * 10) / 16;
        float _x = GlobalPosition.X + characterWidth/4 + characterWidth * X;
        //float _y = (int)(parent.GlobalPosition.Y + parent.Size.Y / 2 - parent.TextRenderer.GetLocalBounds().Height / 2);
        float _y = parent.GlobalPosition.Y + parent.Size.Y / 2 - renderer.GetLocalBounds().Height / 1.5F - parent.Origin.Y;

        renderer.Position = new(_x, _y);
        renderer.DisplayedString = "|";
        renderer.Font = parent.Style.Font;
        renderer.CharacterSize = parent.Style.FontSize;
        renderer.FillColor = GetFillColor();

        Window.Draw(renderer);
    }

    private void UpdateAlpha()
    {
        if (timer > MaxTime)
        {
            alpha = alpha == MaxAlpha ? MinAlpha : MaxAlpha;
            timer = MinTime;
        }

        timer ++;
    }

    private Color GetFillColor()
    {
        Color color = parent.Style.TextColor;
        color.A = alpha;

        return color;
    }
}