using OpenTK.Windowing.Desktop;

namespace EngineSigma.Core.Management;

public static class DisplayManager
{
    public static GameWindow GameWindow = null!;

    public static GameWindow CreateWindow(GameWindowSettings gameWindowSettings,
        NativeWindowSettings nativeWindowSettings)
    {
        GameWindow = new GameWindow(gameWindowSettings, nativeWindowSettings);
        GameWindow.CenterWindow();
        return GameWindow;
    }
}