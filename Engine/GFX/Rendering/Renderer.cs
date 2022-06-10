using Engine.GFX.Shaders;

namespace Engine.GFX.Rendering;

internal sealed class Renderer
{
    private readonly List<IRenderable> _renderables;

    public Renderer()
    {
        _renderables = new List<IRenderable>();
    }

    public void Render(Shader shader)
    {
        foreach (var renderable in _renderables)
        {
            renderable.Render(shader);
        }
        _renderables.Clear();
    }

    public void AddRenderable(IRenderable renderable)
    {
        _renderables.Add(renderable);
    }
}