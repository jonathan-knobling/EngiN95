using EngiN95.Core;

namespace EngiN95;

public static class Program
{
    public static void Main(string[] args)
    {
        Game game = new GameImpl("Test", 1600, 900);
        game.Run();
    }
}