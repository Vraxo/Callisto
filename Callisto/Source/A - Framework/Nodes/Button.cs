﻿using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Font = SFML.Graphics.Font;
using Callisto;

namespace Nodex;

class Button : Node
{
    // Fields

    #region [ - - - FIELDS - - - ]

    // Numbers

    public Vector2f Size = new(100, 25);
    public Vector2f Origin = new(0, 0);
    public Vector2f TextOrigin = new(0, 0);
    public float OutlineThickness = 0;

    // Text

    public string Text = "";
    public uint FontSize = 16;
    public Font Font = FontLoader.Instance.RobotoMono;

    // Colors

    public Color TextColor = Color.White;
    public Color OutlineColor = Color.Black;
    public Color FillColor = new(64, 64, 64);
    public Color HoverFillColor = new(96, 96, 96);
    public Color IdleFillColor = new(64, 64, 64);
    public Color ActiveFillColor = new(48, 48, 48);

    // Etc

    public Action ActionOnClick;
    private bool isActive = false;

    #endregion

    // Public

    public override void Start()
    {
        ConnectToEvents();
    }

    public override void Update()
    {
        base.Update();
        DrawShape();
        DrawText();
    }

    public override void Destroy()
    {
        DisconnectFromEvents();
    }

    public override void Activate()
    {
        base.Activate();

        Start();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        Destroy();
    }

    // Private

    private void DrawShape()
    {
        RectangleShape r = new()
        {
            Position = GlobalPosition,
            Size = Size,
            OutlineThickness = OutlineThickness,
            OutlineColor = OutlineColor,
            FillColor = FillColor,
            Origin = Origin
        };

        r.Draw(Window, RenderStates.Default);
    }

    private void DrawText()
    {
        Text t = new()
        {
            DisplayedString = Text,
            FillColor = TextColor,
            Position = GlobalPosition,
            Font = Font,
            CharacterSize = FontSize
        };

        int x = (int)(GlobalPosition.X - Origin.X + Size.X / 2 - t.GetLocalBounds().Width / 2);
        int y = (int)(GlobalPosition.Y - Origin.Y + Size.Y / 2 - t.GetLocalBounds().Height / 1.25);

        t.Position = new(x, y);

        t.Draw(Window, RenderStates.Default);
    }

    private bool IsMouseOver(Vector2f mousePosition)
    {
        mousePosition = Window.MapPixelToCoords((Vector2i)mousePosition);

        bool matchX1 = mousePosition.X > GlobalPosition.X - Origin.X;
        bool matchX2 = mousePosition.X < GlobalPosition.X + Size.X - Origin.X;

        bool matchY1 = mousePosition.Y > GlobalPosition.Y - Origin.Y;
        bool matchY2 = mousePosition.Y < GlobalPosition.Y + Size.Y - Origin.Y;

        return matchX1 && matchX2 && matchY1 && matchY2;
    }

    // Connection to events

    private void ConnectToEvents()
    {
        Window.MouseMoved += OnMouseMoved;
        Window.MouseButtonPressed += OnMouseClicked;
        Window.MouseButtonReleased += OnMouseReleased;
    }

    private void DisconnectFromEvents()
    {
        Window.MouseMoved -= OnMouseMoved;
        Window.MouseButtonPressed -= OnMouseClicked;
        Window.MouseButtonReleased -= OnMouseReleased;
    }

    // Events

    private void OnMouseMoved(object? sender, MouseMoveEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            FillColor = isActive ? ActiveFillColor : HoverFillColor;
        }
        else
        {
            FillColor = IdleFillColor;
        }
    }

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            isActive = true;
            FillColor = ActiveFillColor;
        }
    }

    private void OnMouseReleased(object? sender, MouseButtonEventArgs e)
    {
        if (IsMouseOver(new(e.X, e.Y)))
        {
            if (isActive)
            {
                ActionOnClick();
            }
        }

        isActive = false;
        FillColor = IdleFillColor;
    }
}