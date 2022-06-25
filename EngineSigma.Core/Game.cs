using EngineSigma.Core.Management;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Core;

public abstract class Game
{
    private readonly GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
    private readonly NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

    public GameWindow GameWindow { get; private set; } = null!;

    protected Game(string windowTitle, int initialWindowWidth, int initialWindowHeight)
    {
        _nativeWindowSettings.Size = new Vector2i(initialWindowWidth, initialWindowHeight);
        _nativeWindowSettings.Title = windowTitle;
        _nativeWindowSettings.API = ContextAPI.OpenGL;
    }

    public void Run()
    {
        Init();
        SetupGameWindow();
    }

    private void SetupGameWindow()
    {
        using var gameWindow = DisplayManager.CreateWindow(_gameWindowSettings, _nativeWindowSettings);
        
        gameWindow.Load += OnLoad;
        gameWindow.UpdateFrame += args =>
        {
            Time.DeltaTimeSpan = TimeSpan.FromSeconds(args.Time);
            Time.TotalGameTimeSpan += TimeSpan.FromSeconds(args.Time);
            OnUpdate();
        };
        gameWindow.RenderFrame += args =>
        {
            OnRender();
            gameWindow.SwapBuffers();
        };
        gameWindow.Resize += args => { GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y); };
        
        GameWindow = gameWindow;
        
        gameWindow.Run();
    }

    protected abstract void Init();
    protected abstract void OnLoad();
    protected abstract void OnUpdate();
    protected abstract void OnRender();
}