using EngineSigma.Core;
using OpenTK.Windowing.Common;

namespace Game;

public class Game : GameManager
{
    //gets called directly after the window is updated
    protected override void Update(FrameEventArgs args)
    {
        base.Update(args);
    }

    //gets called directly after the window is rendered
    protected override void Render(FrameEventArgs args)
    {
        base.Render(args);
    }
}