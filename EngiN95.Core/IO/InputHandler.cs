using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

/// <summary>
/// Provides an API for Input related Data
/// </summary>
public sealed class InputHandler : IInputHandler
{
    public IMouseInputHandler MouseInput { get; }
    public IKeyboardInputHandler KeyboardInput { get; }

    public InputHandler(IMouseInputHandler mouseInput, IKeyboardInputHandler keyboardInput)
    {
        MouseInput = mouseInput;
        KeyboardInput = keyboardInput;
    }

    /// <inheritdoc cref="IInputHandler.MousePosition"/>
    public Vector2 MousePosition => MouseInput.Position;

    /// <inheritdoc cref="IInputHandler.PreviousMousePosition"/>
    public Vector2 PreviousMousePosition => MouseInput.PreviousPosition;

    /// <inheritdoc cref="IInputHandler.MousePositionDelta"/>
    public Vector2 MousePositionDelta => MouseInput.PositionDelta;

    /// <inheritdoc cref="IInputHandler.MouseScroll"/>
    public Vector2 MouseScroll => MouseInput.Scroll;

    /// <inheritdoc cref="IInputHandler.PreviousMouseScroll"/>
    public Vector2 PreviousMouseScroll => MouseInput.PreviousScroll;

    /// <inheritdoc cref="IInputHandler.MouseScrollDelta"/>
    public Vector2 MouseScrollDelta => MouseInput.ScrollDelta;
    
    public float MouseX => MousePosition.X;
    public float MouseY => MousePosition.Y;

    public float PreviousMouseX => PreviousMousePosition.X;
    public float PreviousMouseY => PreviousMousePosition.Y;
    
    /// <inheritdoc cref="IInputHandler.IsMouseButtonDown"/>
    public bool IsMouseButtonDown(MouseButton button) => MouseInput.IsButtonDown(button);

    /// <inheritdoc cref="IInputHandler.WasMouseButtonDown"/>
    public bool WasMouseButtonDown(MouseButton button) => MouseInput.WasButtonDown(button);

    /// <inheritdoc cref="IInputHandler.IsMouseButtonPressed"/>
    public bool IsMouseButtonPressed(MouseButton button) => MouseInput.IsButtonPressed(button);

    /// <inheritdoc cref="IInputHandler.IsMouseButtonReleased"/>
    public bool IsMouseButtonReleased(MouseButton button) => MouseInput.IsButtonReleased(button);
    
    /// <inheritdoc cref="IInputHandler.IsAnyMouseButtonDown"/>
    public bool IsAnyMouseButtonDown() => MouseInput.IsAnyButtonDown();

    /// <inheritdoc cref="IInputHandler.IsKeyDown"/>
    public bool IsKeyDown(Keys key) => KeyboardInput.IsKeyDown(key);

    /// <inheritdoc cref="IInputHandler.WasKeyDown"/>
    public bool WasKeyDown(Keys key) => KeyboardInput.WasKeyDown(key);

    /// <inheritdoc cref="IInputHandler.IsKeyPressed"/>
    public bool IsKeyPressed(Keys key) => KeyboardInput.IsKeyPressed(key);

    /// <inheritdoc cref="IInputHandler.IsKeyReleased"/>
    public bool IsKeyReleased(Keys key) => KeyboardInput.IsKeyReleased(key);

    /// <inheritdoc cref="IInputHandler.IsAnyKeyDown"/>
    public bool IsAnyKeyDown() => KeyboardInput.IsAnyKeyDown();
    
    /// <inheritdoc cref="IInputHandler.MovementDirection"/>
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