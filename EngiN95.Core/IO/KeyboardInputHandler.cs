using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

public sealed class KeyboardInputHandler : IKeyboardInputHandler
{
    private readonly KeyboardState keyboardState;

    public KeyboardInputHandler(KeyboardState keyboardState)
    {
        this.keyboardState = keyboardState;
    }

    public bool IsKeyDown(Keys key)
    {
        return keyboardState.IsKeyDown(key);
    }

    public bool WasKeyDown(Keys key)
    {
        return keyboardState.WasKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return keyboardState.IsKeyPressed(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return keyboardState.IsKeyReleased(key);
    }

    public bool IsAnyKeyDown()
    {
        return keyboardState.IsAnyKeyDown;
    }
}