using OpenTK.Mathematics;

namespace EngineSigma.Core.Rendering;

public interface IRenderer
{
    void Render(Vertex[] vertices, Texture texture, Matrix4 transform);
}