using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine.Rendering;

public sealed class Renderer
{
    private readonly List<Mesh> _meshes;

    public Renderer()
    {
        _meshes = new List<Mesh>();
    }

    public void Render()
    {
        foreach (var mesh in _meshes)
        {
            GL.BindVertexArray(mesh.VertexArray.VertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, mesh.IndexBuffer.IndexBufferHandle);
        
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }

    public void AddMesh(Mesh mesh)
    {
        _meshes.Add(mesh);
    }
}