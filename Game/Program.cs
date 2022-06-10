namespace Game;

public static class Program
{
    private static void Main()
    {
        using var game = new EngineSigma.Core.Game();
        game.Run();
    }
}