using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineSigma.Core.IO;

public sealed class InputHandler
{
    private IMouseInputHandler MouseInput { get; }
    private IKeyboardInputHandler KeyboardInput { get; }

    public InputHandler(IMouseInputHandler mouseInput, IKeyboardInputHandler keyboardInput)
    {
        MouseInput = mouseInput;
        KeyboardInput = keyboardInput;
    }


    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the current frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public Vector2 MousePosition => MouseInput.Position;

    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the previous frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public Vector2 PreviousMousePosition => MouseInput.PreviousPosition;

    /// <summary>
    /// Gets a Vector2 representing the amount that the mouse moved since the last frame.
    /// This does not necessarily correspond to pixels, for example in the case of raw input.
    /// </summary>
    public Vector2 MousePositionDelta => MouseInput.PositionDelta;

    /// <summary>
    /// Get a Vector2 representing the position of the mouse wheel.
    /// </summary>
    public Vector2 MouseScroll => MouseInput.Scroll;

    /// <summary>
    /// Get a Vector2 representing the previous position of the mouse wheel.
    /// </summary>
    public Vector2 PreviousMouseScroll => MouseInput.PreviousScroll;

    /// <summary>
    /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
    /// </summary>
    public Vector2 MouseScrollDelta => MouseInput.ScrollDelta;
    
    public float MouseX => MousePosition.X;
    public float MouseY => MousePosition.Y;

    public float PreviousMouseX => PreviousMousePosition.X;
    public float PreviousMouseY => PreviousMousePosition.Y;
    
    /// <summary>
    /// Gets whether this mouse button is currently down
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsMouseButtonDown(MouseButton button) => MouseInput.IsButtonDown(button);

    /// <summary>
    /// Gets whether this button was down in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool WasMouseButtonDown(MouseButton button) => MouseInput.WasButtonDown(button);

    /// <summary>
    /// Gets whether this key is down in this frame but wasnt in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsMouseButtonPressed(MouseButton button) => MouseInput.IsButtonPressed(button);

    /// <summary>
    /// Gets whether this button is released in the current frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public bool IsMouseButtonReleased(MouseButton button) => MouseInput.IsButtonReleased(button);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>If any mouse button is currently pressed</returns>
    public bool IsAnyMouseButtonDown() => MouseInput.IsAnyButtonDown();

    /// <summary>
    /// Gets whether this key is down in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsKeyDown(Keys key) => KeyboardInput.IsKeyDown(key);

    /// <summary>
    /// Gets whether this key was down in the previous frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool WasKeyDown(Keys key) => KeyboardInput.WasKeyDown(key);

    /// <summary>
    /// Gets whether this key is down in the current frame but wasnt in the last frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsKeyPressed(Keys key) => KeyboardInput.IsKeyPressed(key);

    /// <summary>
    /// Gets whether this key is released in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsKeyReleased(Keys key) => KeyboardInput.IsKeyReleased(key);

    /// <summary>
    /// 
    /// </summary>
    /// <returns>If any key is currently pressed</returns>
    public bool IsAnyKeyDown() => KeyboardInput.IsAnyKeyDown();
    
    /// <summary>
    /// Gets the current MovementDirection
    /// x = 1 means right | y = 1 means up
    /// 0 means no movement in this axis and minus one means left or down
    /// </summary>
    public Vector2 MovementDirection
    {
        get
        {
            var movementDirection = Vector2.Zero;
            if (IsKeyDown(Keys.W)) movementDirection.Y += 1;
            if (IsKeyDown(Keys.S)) movementDirection.Y -= 1;
            if (IsKeyDown(Keys.A)) movementDirection.X -= 1;
            if (IsKeyDown(Keys.D)) movementDirection.X += 1;
            return movementDirection;
        }
    }
}