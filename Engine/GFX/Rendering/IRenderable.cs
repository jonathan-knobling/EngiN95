using Engine.GFX.Shaders;

namespace Engine.GFX.Rendering;

internal interface IRenderable
{
    void Render(Shader shader);
}