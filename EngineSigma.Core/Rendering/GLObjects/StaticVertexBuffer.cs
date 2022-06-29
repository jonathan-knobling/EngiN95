using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class StaticVertexBuffer : IVertexBuffer
{
    private readonly IGLWrapper _glWrapper;
    public int Handle { get; }

    private bool _disposed;

    public StaticVertexBuffer(IGLWrapper glWrapper, Vertex[] vertices)
    {
        _glWrapper = glWrapper;
        Handle = _glWrapper.GenBuffer();
        _glWrapper.BindBuffer(BufferTarget.ArrayBuffer, Handle);
        _glWrapper.BufferData(BufferTarget.ArrayBuffer, vertices.Length * Vertex.Size, vertices, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        _glWrapper.BindBuffer(BufferTarget.ArrayBuffer, Handle);
    }

    public void UnBind()
    {
        _glWrapper.BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    ~StaticVertexBuffer()
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