using OpenTK.Mathematics;

namespace EngiN95.Core.Rendering;

public interface IRenderer
{
    void Render(Vertex[] vertices, Texture texture, Matrix4 transform);
}