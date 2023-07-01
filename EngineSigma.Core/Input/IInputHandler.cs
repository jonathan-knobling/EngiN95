using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineSigma.Core.Input;

public interface IInputHandler
{
    IMouseInputHandler MouseInput { get; }
    IKeyboardInputHandler KeyboardInput { get; }

    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the current frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    Vector2 MousePosition { get; }

    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the previous frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    Vector2 PreviousMousePosition { get; }

    /// <summary>
    /// Gets a Vector2 representing the amount that the mouse moved since the last frame.
    /// This does not necessarily correspond to pixels, for example in the case of raw input.
    /// </summary>
    Vector2 MousePositionDelta { get; }

    /// <summary>
    /// Get a Vector2 representing the position of the mouse wheel.
    /// </summary>
    Vector2 MouseScroll { get; }

    /// <summary>
    /// Get a Vector2 representing the previous position of the mouse wheel.
    /// </summary>
    Vector2 PreviousMouseScroll { get; }

    /// <summary>
    /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
    /// </summary>
    Vector2 MouseScrollDelta { get; }

    float MouseX { get; }
    float MouseY { get; }
    float PreviousMouseX { get; }
    float PreviousMouseY { get; }

    /// <summary>
    /// Gets the current MovementDirection
    /// x = 1 means right | y = 1 means up
    /// 0 means no movement in this axis and minus one means left or down
    /// </summary>
    Vector2 MovementDirection { get; }

    /// <summary>
    /// Gets whether this mouse button is currently down
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsMouseButtonDown(MouseButton button);

    /// <summary>
    /// Gets whether this button was down in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool WasMouseButtonDown(MouseButton button);

    /// <summary>
    /// Gets whether this key is down in this frame but wasnt in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsMouseButtonPressed(MouseButton button);

    /// <summary>
    /// Gets whether this button is released in the current frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    bool IsMouseButtonReleased(MouseButton button);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>If any mouse button is currently pressed</returns>
    bool IsAnyMouseButtonDown();

    /// <summary>
    /// Gets whether this key is down in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool IsKeyDown(Keys key);

    /// <summary>
    /// Gets whether this key was down in the previous frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool WasKeyDown(Keys key);

    /// <summary>
    /// Gets whether this key is down in the current frame but wasnt in the last frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool IsKeyPressed(Keys key);

    /// <summary>
    /// Gets whether this key is released in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    bool IsKeyReleased(Keys key);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>If any key is currently pressed</returns>
    bool IsAnyKeyDown();
}