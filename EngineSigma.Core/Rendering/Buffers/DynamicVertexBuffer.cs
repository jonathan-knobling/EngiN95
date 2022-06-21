using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class DynamicVertexBuffer : IBuffer
{
    public int Handle { get; }
    public int Elements { get; private set; }
    private bool _disposed;

    /// <summary>
    /// Creates a new Dynamic VertexBuffer
    /// </summary>
    /// <param name="bufferCapacity">How many Vertices can be stored in this buffer</param>
    public DynamicVertexBuffer(int bufferCapacity = 1024)
    {
        Handle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        GL.BufferData(BufferTarget.ArrayBuffer, Vertex.Size * bufferCapacity, IntPtr.Zero, BufferUsageHint.DynamicDraw);
    }

    public void BufferData(Vertex[] data)
    {
        if (data.Length == 0) return;

        Elements = data.Length;
        Bind();
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * Vertex.Size, data);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, Handle);
    }

    public void UnBind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    
    ~DynamicVertexBuffer()
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