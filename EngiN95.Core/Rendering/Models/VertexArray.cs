using OpenTK.Graphics.OpenGL4;

namespace EngiN95.Core.Rendering;

public class VertexArray : IVertexArray
{
    private readonly IGLWrapper _glWrapper;
    public Handle Handle { get; }

    private bool _disposed;

    public VertexArray(IGLWrapper glWrapper)
    {
        _glWrapper = glWrapper;
        Handle = _glWrapper.GenVertexArray();
        Bind();
        
        //Position
        _glWrapper.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Size, 0);
        _glWrapper.EnableVertexAttribArray(0);
        
        //TexCoords
        _glWrapper.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, Vertex.Size, 3 * sizeof(float));
        _glWrapper.EnableVertexAttribArray(1);
        
        //Color
        _glWrapper.VertexAttribPointer(2, 4, VertexAttribPointerType.Float, false, Vertex.Size, 5 * sizeof(float));
        _glWrapper.EnableVertexAttribArray(2);
    }

    public void Bind()
    {
        _glWrapper.BindVertexArray(Handle);
    }

    public void UnBind()
    {
        _glWrapper.BindVertexArray(0);
    }

    ~VertexArray()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _glWrapper.DeleteVertexArray(Handle);
        GC.SuppressFinalize(this);
    }
}