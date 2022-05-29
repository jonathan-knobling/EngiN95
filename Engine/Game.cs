using EngineSigma.Engine.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Engine;

public class Game: IDisposable
{
    private readonly Window _window;

    public Game()
    {
        var settings = new NativeWindowSettings()
        {
            Title = "Engine Sigma",
            StartVisible = false,
            StartFocused = true,
            APIVersion = new Version(3,3),
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            WindowBorder = WindowBorder.Hidden,
            Size = new Vector2i(1920, 1080)
        };
        
        _window = new Window(settings);
    }

    public void Run()
    {
        _window.Run();
    }

    public void Dispose()
    {
        _window.Dispose();
    }
}