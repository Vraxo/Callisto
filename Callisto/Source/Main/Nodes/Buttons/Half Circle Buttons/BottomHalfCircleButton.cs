using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Callisto;

class BottomHalfCircleButton : HalfCircleButton
{
    // Public

    public override void Start()
    {
        base.Start();

        renderTextureY = -Radius;
    }

    // Private

    protected override bool IsMouseOver(Vector2f mousePosition)
    {
        if (mousePosition.Y < GlobalPosition.Y) return false;

        return IsInRange(mousePosition);
    }
}