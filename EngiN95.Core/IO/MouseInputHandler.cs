using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

public class MouseInputHandler : IMouseInputHandler
{
    private readonly MouseState mouseState;

    public MouseInputHandler(MouseState mouseState)
    {
        this.mouseState = mouseState;
    }

    public Vector2 Position => mouseState.Position;

    public Vector2 PreviousPosition => mouseState.PreviousPosition;

    public Vector2 PositionDelta => mouseState.Delta;

    public Vector2 Scroll => mouseState.Scroll;

    public Vector2 PreviousScroll => mouseState.PreviousScroll;

    public Vector2 ScrollDelta => mouseState.ScrollDelta;

    public bool IsButtonDown(MouseButton button)
    {
        return mouseState.IsButtonDown(button);
    }

    public bool WasButtonDown(MouseButton button)
    {
        return mouseState.WasButtonDown(button);
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return mouseState.IsButtonPressed(button);
    }

    public bool IsButtonReleased(MouseButton button)
    {
        return mouseState.IsButtonReleased(button);
    }

    public bool IsAnyButtonDown()
    {
        return mouseState.IsAnyButtonDown;
    }
}