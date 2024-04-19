using OpenTK.Windowing.GraphicsLibraryFramework;

namespace EngiN95.Core.IO;

public interface IKeyboardInputHandler
{
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
    /// Gets whether this key is down in the current frame but wasn't in the last frame
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