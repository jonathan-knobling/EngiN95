using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineSigma.IO;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

public static class Input
{
    //Input States passed from Window
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
    ///     Gets whether this mouse button is currently down
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool IsMouseButtonDown(MouseButton button) => MouseState.IsButtonDown(button);
    
    /// <summary>
    ///     Gets whether this button was down in the last frame
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
    ///     Gets whether this button is released in the current frame
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public static bool IsMouseButtonReleased(MouseButton button) => MouseState.IsButtonReleased(button);

    /// <summary>
    ///     Gets whether this key is down in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyDown(Keys key) => KeyboardState.IsKeyDown(key);

    /// <summary>
    ///     Gets whether this key was down in the previous frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool WasKeyDown(Keys key) => KeyboardState.WasKeyDown(key);

    /// <summary>
    ///     Gets whether this key is down in the current frame but wasnt in the last frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyPressed(Keys key) => KeyboardState.IsKeyPressed(key);

    /// <summary>
    ///     Gets whether this key is released in the current frame
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static bool IsKeyReleased(Keys key) => KeyboardState.IsKeyReleased(key);

    public static bool IsAnyMouseButtonDown() => MouseState.IsAnyButtonDown;
    public static bool IsAnyKeyDown() => KeyboardState.IsAnyKeyDown;
    
    /// <summary>
    ///     Gets the current MovementDirection
    ///     x = 1 means right | y = 1 means up
    ///     0 means no movement in this axis and minus one means left or down
    /// </summary>
    public static Vector2 MovementDirection { get; internal set; }

    internal static void Update()
    {
        //get wasd keys
        var w = IsKeyDown(Keys.W);
        var a = IsKeyDown(Keys.A);
        var s = IsKeyDown(Keys.S);
        var d = IsKeyDown(Keys.D);

        //temporary movement direction vector
        var dir = new Vector2
        {
            //set x direction
            X = a switch
            {
                //a && !d
                true when !d => 1f,
                //!a && d
                false when d => -1f,
                //!(a && d)
                _ => 0f
            },
            //set y direction
            Y = w switch
            {
                //w && !s
                true when !s => 1f,
                //!w && s
                false when s => -1f,
                //!(w && s)
                _ => 0f
            }
        };

        MovementDirection = dir;
    }
}