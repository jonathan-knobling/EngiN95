using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineSigma.Core.IO;

public static class Input
{
    internal static MouseState MouseState { private get; set; } = null!;
    internal static KeyboardState KeyboardState { private get; set; } = null!;
    
    
    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the current frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public static Vector2 MousePosition => MouseState.Position;

    /// <summary>
    /// Gets a Vector2 representing the absolute position of the pointer
    /// in the previous frame, relative to the top-left corner of the contents of the window.
    /// </summary>
    public static Vector2 PreviousMousePosition => MouseState.PreviousPosition;

    /// <summary>
    /// Gets a Vector2 representing the amount that the mouse moved since the last frame.
    /// This does not necessarily correspond to pixels, for example in the case of raw input.
    /// </summary>
    public static Vector2 MousePositionDelta => MouseState.Delta;

    /// <summary>
    /// Get a Vector2 representing the position of the mouse wheel.
    /// </summary>
    public static Vector2 MouseScroll => MouseState.Scroll;

    /// <summary>
    /// Get a Vector2 representing the previous position of the mouse wheel.
    /// </summary>
    public static Vector2 PreviousMouseScroll => MouseState.PreviousScroll;

    /// <summary>
    /// Get a Vector2 representing the amount that the mouse wheel moved since the last frame.
    /// </summary>
    public static Vector2 MouseScrollDelta => MouseState.ScrollDelta;
    
    public static float MouseX => MousePosition.X;
    public static float MouseY => MousePosition.Y;

    public static float PreviousMouseX => PreviousMousePosition.X;
    public static float PreviousMouseY => PreviousMousePosition.Y;
    
    /// <summary>
    /// Gets whether this mouse button is currently down
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool IsMouseButtonDown(MouseButton button) => MouseState.IsButtonDown(button);
    
    /// <summary>
    /// Gets whether this button was down in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool WasMouseButtonDown(MouseButton button) => MouseState.WasButtonDown(button);
    
    /// <summary>
    /// Gets whether this key is down in this frame but wasnt in the last frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool IsMouseButtonPressed(MouseButton button) => MouseState.IsButtonPressed(button);
    
    /// <summary>
    /// Gets whether this button is released in the current frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool IsMouseButtonReleased(MouseButton button) => MouseState.IsButtonReleased(button);

    /// <summary>
    /// Gets whether this key is down in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

    /// <summary>
    /// Gets whether this key was down in the previous frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool WasKeyDown(Keys key) => KeyboardState.WasKeyDown(key);

    /// <summary>
    /// Gets whether this key is down in the current frame but wasnt in the last frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);

    /// <summary>
    /// Gets whether this key is released in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyReleased(Keys key) => KeyboardState.IsKeyReleased(key);

    public static bool IsAnyMouseButtonDown() => MouseState.IsAnyButtonDown;
    public static bool IsAnyKeyDown() => KeyboardState.IsAnyKeyDown;
    
    /// <summary>
    /// Gets the current MovementDirection
    /// x = 1 means right | y = 1 means up
    /// 0 means no movement in this axis and minus one means left or down
    /// </summary>
    public static Vector2 MovementDirection { get; internal set; }

    internal static void Update()
    {
        var movementDirection = Vector2.Zero;
        if (IsKeyDown(Keys.W)) movementDirection.Y += 1;
        if (IsKeyDown(Keys.S)) movementDirection.Y -= 1;
        if (IsKeyDown(Keys.A)) movementDirection.X -= 1;
        if (IsKeyDown(Keys.D)) movementDirection.X += 1;
        MovementDirection = movementDirection;
    }
}