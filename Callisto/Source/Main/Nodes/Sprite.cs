﻿using SFML.System;
using SFML.Graphics;

namespace Callisto;

class Sprite : Node
{
    // Fields

    public Vector2f Size;
    public Vector2f Origin;
    public Texture Texture;

    private RectangleShape spriteRenderer = new();

    // Public

    public override void Update()
    {
        base.Update();

        spriteRenderer.Position = Position;
        spriteRenderer.Size = Size;
        spriteRenderer.Origin = Origin;
        spriteRenderer.Texture = Texture;

        Window.Draw(spriteRenderer);
    }
}