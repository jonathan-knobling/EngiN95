using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class VertexBuffer : IVertexBuffer
{
    private readonly IGLWrapper _glWrapper;
    public Handle Handle { get; }
    public int Elements { get; private set; }
    private bool _disposed;

    /// <summary>
    /// Creates a new Dynamic VertexBuffer
    /// </summary>
    /// <param name="glWrapper">A Wrapper around OpenTKs GL API</param>
    /// <param name="bufferCapacity">How many Vertices can be stored in this buffer</param>
    public VertexBuffer(IGLWrapper glWrapper, int bufferCapacity = 1024)
    {
        _glWrapper = glWrapper;
        Handle = _glWrapper.GenBuffer();
        Bind();
        _glWrapper.BufferData(BufferTarget.ArrayBuffer, Vertex.Size * bufferCapacity, IntPtr.Zero, BufferUsageHint.DynamicDraw);
    }

    public void BufferData(Vertex[] data)
    {
        if (data.Length == 0) return;

        Elements = data.Length;
        Bind();
        _glWrapper.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * Vertex.Size, data);
    }

    public void Bind()
    {
        _glWrapper.BindBuffer(BufferTarget.ArrayBuffer, Handle);
    }

    public void UnBind()
    {
        _glWrapper.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }
    
    ~VertexBuffer()
    {
        Dispose();
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _glWrapper.DeleteBuffer(Handle);
        GC.SuppressFinalize(this);
    }
}