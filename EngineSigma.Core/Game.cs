using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Core;

public abstract class Game
{
    protected string WindowTitle { get; set; }
    protected int InitialWindowWidth { get; set; }
    protected int InitialWindowHeight { get; set; }
    
    private GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
    private NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

    protected Game(string windowTitle, int initialWindowWidth, int initialWindowHeight)
    {
        WindowTitle = windowTitle;
        InitialWindowWidth = initialWindowWidth;
        InitialWindowHeight = initialWindowHeight;

        _nativeWindowSettings.Size = new Vector2i(initialWindowWidth, initialWindowHeight);
        _nativeWindowSettings.Title = WindowTitle;
    }

    public void Run()
    {
        Init();
        using GameWindow gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings);
        gameWindow.Load += OnLoad;
        gameWindow.UpdateFrame += args =>
        {
            Time.DeltaTime = TimeSpan.FromSeconds(args.Time);
            Time.TotalGameTime += TimeSpan.FromSeconds(args.Time);
            OnUpdate();
        };
        gameWindow.RenderFrame += args =>
        {
            OnRender();
            gameWindow.SwapBuffers();
        };
        gameWindow.Resize += args =>
        {
            GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
        };
        gameWindow.Run();
    }

    protected abstract void Init();
    protected abstract void OnLoad();
    protected abstract void OnUpdate();
    protected abstract void OnRender();
}