namespace EngineSigma.Engine;

public class Game: IDisposable
{
    private readonly Window _window;

    public Game()
    {
        _window = new Window();
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