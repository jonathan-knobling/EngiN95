using EngineSigma.Core.IO;
using EngineSigma.Core.Management;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Core;

public abstract class Game
{
    private readonly GameWindowSettings _gameWindowSettings = GameWindowSettings.Default;
    private readonly NativeWindowSettings _nativeWindowSettings = NativeWindowSettings.Default;

    protected GameWindow GameWindow { get; private set; } = null!;
    protected InputHandler InputHandler { get; private set; } = null!;

    protected Game(string windowTitle, int initialWindowWidth, int initialWindowHeight)
    {
        _nativeWindowSettings.Size = new Vector2i(initialWindowWidth, initialWindowHeight);
        _nativeWindowSettings.Title = windowTitle;
        _nativeWindowSettings.API = ContextAPI.OpenGL;
        _nativeWindowSettings.APIVersion = new Version(4, 6);
        
        var img = ResourceManager.GetImage("Resources/Sprites/stone.png");
        _nativeWindowSettings.Icon = new WindowIcon(new Image(img.Width, img.Height, img.PixelData));
    }

    public void Run()
    {
        Init();
        SetupGameWindow();
    }

    private void SetupGameWindow()
    {
        var gameWindow = new GameWindow(_gameWindowSettings, _nativeWindowSettings);

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
        gameWindow.Resize += args =>
        {
            GL.Viewport(0, 0, gameWindow.Size.X, gameWindow.Size.Y);
        };
        
        GameWindow = gameWindow;
        InputHandler = new InputHandler(
            new MouseInputHandler(gameWindow.MouseState),
            new KeyboardInputHandler(gameWindow.KeyboardState));
        
        gameWindow.CenterWindow();
        gameWindow.Run();
    }

    protected abstract void Init();
    protected abstract void OnLoad();
    protected abstract void OnUpdate();
    protected abstract void OnRender();
}