namespace Game;

public static class Program
{
    private static void Main()
    {
        using var game = new Engine.Game();
        game.Run();
    }
}