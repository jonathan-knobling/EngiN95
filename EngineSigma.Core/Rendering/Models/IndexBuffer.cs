using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class IndexBuffer : IIndexBuffer
{
    private readonly IGLWrapper _glWrapper;
    public Handle Handle { get; }

    private bool _disposed;

    public IndexBuffer(IGLWrapper glWrapper, uint[] indices)
    {
        _glWrapper = glWrapper;
        _disposed = false;
        Handle = _glWrapper.GenBuffer();
        _glWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
        _glWrapper.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);
    }

    public void Bind()
    {
        _glWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, Handle);
    }

    public void UnBind()
    {
        _glWrapper.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
    }

    ~IndexBuffer()
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