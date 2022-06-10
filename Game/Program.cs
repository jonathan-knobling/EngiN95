namespace Game;

public static class Program
{
    private static void Main()
    {
        //hier am besten nicht ändern
        using var game = new EngineSigma.Core.GameManager();
        game.Run();
    }
}