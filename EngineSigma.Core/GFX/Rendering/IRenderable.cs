using EngineSigma.Core.GFX.Shaders;

namespace EngineSigma.Core.GFX.Rendering;

internal interface IRenderable
{
    void Render(Shader shader);
}