﻿using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Callisto;

class CircleButton : Node
{
    // Fields

    public float Radius;
    public Vector2f Origin;
    public bool IsVisible;

    protected Action actionOnClick;

    private bool isSelected = false;

    // Public

    public override void Start()
    {
        base.Start();

        ConnectToEvents();
    }

    public override void Update()
    {
        base.Update();

        DrawDebugCircle();
    }

    public override void Destroy()
    {
        base.Destroy();

        Window.MouseButtonPressed -= OnMouseClicked;
        Window.MouseButtonReleased -= OnMouseClicked;
    }

    public override void Activate()
    {
        base.Activate();

        ConnectToEvents();
    }

    public override void Deactivate()
    {
        base.Deactivate();

        DisconnectFromEvent();
    }

    // Private

    private void DrawDebugCircle()
    {
        if (IsVisible)
        {
            CircleShape c = new()
            {
                Position = Position,
                Radius = Radius,
                Origin = Origin,
                OutlineColor = Color.Blue,
            };

            Window.Draw(c);
        }
    }

    private bool IsInRange(Vector2f position)
    {
        bool isInRange = GetDistance(GlobalPosition, position) < Radius;
        return isInRange;
    }

    private float GetDistance(Vector2f point1, Vector2f point2)
    {
        float xDistance = MathF.Pow(point2.X - point1.X, 2);
        float yDistance = MathF.Pow(point2.Y - point1.Y, 2);

        float distance = MathF.Sqrt(xDistance + yDistance);

        return distance;
    }

    // Events

    private void ConnectToEvents()
    {
        Window.MouseButtonPressed += OnMouseClicked;
        Window.MouseButtonReleased += OnMouseReleased;
    }

    private void DisconnectFromEvent()
    {
        Window.MouseButtonPressed -= OnMouseClicked;
        Window.MouseButtonReleased -= OnMouseReleased;
    }

    private void OnMouseClicked(object? sender, MouseButtonEventArgs e)
    {
        if (IsInRange(new(e.X, e.Y)))
        {
            isSelected = true;
        }
    }

    private void OnMouseReleased(object? sender, MouseButtonEventArgs e)
    {
        if (IsInRange(new(e.X, e.Y)))
        {
            if (isSelected)
            {
                actionOnClick();
            }
        }

        isSelected = false;
    }
}