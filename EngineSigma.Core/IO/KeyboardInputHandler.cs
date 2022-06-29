using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngineSigma.Core.IO;

public sealed class KeyboardInputHandler : IKeyboardInputHandler
{
    private readonly KeyboardState _keyboardState;

    public KeyboardInputHandler(KeyboardState keyboardState)
    {
        _keyboardState = keyboardState;
    }

    public bool IsKeyDown(Keys key)
    {
        return _keyboardState.IsKeyDown(key);
    }

    public bool WasKeyDown(Keys key)
    {
        return _keyboardState.WasKeyDown(key);
    }

    public bool IsKeyPressed(Keys key)
    {
        return _keyboardState.IsKeyPressed(key);
    }

    public bool IsKeyReleased(Keys key)
    {
        return _keyboardState.IsKeyReleased(key);
    }

    public bool IsAnyKeyDown()
    {
        return _keyboardState.IsAnyKeyDown;
    }
}