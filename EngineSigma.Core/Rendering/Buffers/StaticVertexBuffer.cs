using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class StaticVertexBuffer : IBuffer
{
    public int Handle { get; }

    private bool _disposed;

    public StaticVertexBuffer(Vertex[] vertices)
    {
        Handle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.Size, vertices, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
    }

    public void UnBind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    ~StaticVertexBuffer()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GL.DeleteBuffer(Handle);
        GC.SuppressFinalize(this);
    }
}