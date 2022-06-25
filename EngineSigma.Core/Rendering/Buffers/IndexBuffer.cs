using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class IndexBuffer : IBuffer
{
    public int Handle { get; }

    private bool _disposed;

    public IndexBuffer(uint[] indices)
    {
        _disposed = false;
        Handle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
    }

    public void UnBind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    ~IndexBuffer()
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