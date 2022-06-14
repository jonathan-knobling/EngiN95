using EngineSigma.Core;
using EngineTest.Implementations;

namespace EngineTest;

public static class Program
{
    public static void Main(string[] args)
    {
        Game game = new TextureTest("Test", 1600, 900);
        game.Run();
    }
}