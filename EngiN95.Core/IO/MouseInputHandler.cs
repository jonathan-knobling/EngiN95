using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

public class MouseInputHandler : IMouseInputHandler
{
    private readonly MouseState _mouseState;

    public MouseInputHandler(MouseState mouseState)
    {
        _mouseState = mouseState;
    }

    public Vector2 Position => _mouseState.Position;

    public Vector2 PreviousPosition => _mouseState.PreviousPosition;

    public Vector2 PositionDelta => _mouseState.Delta;

    public Vector2 Scroll => _mouseState.Scroll;

    public Vector2 PreviousScroll => _mouseState.PreviousScroll;

    public Vector2 ScrollDelta => _mouseState.ScrollDelta;

    public bool IsButtonDown(MouseButton button)
    {
        return _mouseState.IsButtonDown(button);
    }

    public bool WasButtonDown(MouseButton button)
    {
        return _mouseState.WasButtonDown(button);
    }

    public bool IsButtonPressed(MouseButton button)
    {
        return _mouseState.IsButtonPressed(button);
    }

    public bool IsButtonReleased(MouseButton button)
    {
        return _mouseState.IsButtonReleased(button);
    }

    public bool IsAnyButtonDown()
    {
        return _mouseState.IsAnyButtonDown;
    }
}