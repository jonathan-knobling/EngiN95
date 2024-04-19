using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

public interface IMouseInputHandler
{
    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the current frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public Vector2 Position { get; }

    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the previous frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public Vector2 PreviousPosition { get; }

    /// <summary>
    /// Gets a Vector2 representing the amount that the mouse moved since the last frame.
    /// This does not necessarily correspond to pixels, for example in the case of raw input.
    /// </summary>
    public Vector2 PositionDelta { get; }

    /// <summary>
    /// Get a Vector2 representing the position of the mouse wheel.
    /// </summary>
    public Vector2 Scroll { get; }

    /// <summary>
    /// Get a Vector2 representing the previous position of the mouse wheel.
    /// </summary>
    public Vector2 PreviousScroll { get; }

    /// <summary>
    /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
    /// </summary>
    public Vector2 ScrollDelta { get; }

    /// <summary>
    /// Gets whether this mouse button is currently down
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsButtonDown(MouseButton button);

    /// <summary>
    /// Gets whether this button was down in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool WasButtonDown(MouseButton button);

    /// <summary>
    /// Gets whether this key is down in this frame but wasn't in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsButtonPressed(MouseButton button);

    /// <summary>
    /// Gets whether this button is released in the current frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsButtonReleased(MouseButton button);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>If any mouse button is currently pressed</returns>
    bool IsAnyButtonDown();
}