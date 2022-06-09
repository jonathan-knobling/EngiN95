using EngineSigma.GFX.Shaders;

namespace EngineSigma.GFX.Rendering;

internal interface IRenderable
{
    void Render(Shader shader);
}