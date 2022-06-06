using EngineSigma.Engine.Rendering.shaders;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine.Rendering;

public sealed class Renderer
{
    private readonly List<Sprite> _meshes;

    public Renderer()
    {
        _meshes = new List<Sprite>();
    }

    public void Render(Shader shader)
    {
        foreach (var mesh in _meshes)
        {
            GL.BindVertexArray(mesh.VertexArray.VertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.IndexBuffer.IndexBufferHandle);

            shader.SetMatrix4("transform", mesh.Transform);
        
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }

    public void AddMesh(Sprite sprite)
    {
        _meshes.Add(sprite);
    }
}