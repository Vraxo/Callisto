﻿using SFML.Graphics;

namespace Nodex;

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
            x = value;
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

        if (!parent.IsSelected) return;

        Draw();
        SetAlpha();
    }

    // Private

    private void Draw()
    {
        float characterWidth = (parent.Style.FontSize * 10) / 16;
        float _x = GlobalPosition.X + characterWidth * X;
        float _y = GlobalPosition.Y;

        Console.WriteLine(X);

        renderer.Position = new(_x, _y);
        renderer.DisplayedString = "|";
        renderer.Font = parent.Style.Font;
        renderer.CharacterSize = parent.Style.FontSize;
        renderer.FillColor = new(255, 255, 255, alpha);

        Window.Draw(renderer);
    }

    private void SetAlpha()
    {
        if (timer > MaxTime)
        {
            alpha = alpha == MaxAlpha ? MinAlpha : MaxAlpha;
            timer = MinTime;
        }

        timer ++;
    }
}