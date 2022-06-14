using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class VertexArray : IBuffer
{
    public int Handle { get; }

    private bool _disposed;

    public VertexArray()
    {
        Handle = GL.GenVertexArray();
        Bind();
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    public void Bind()
    {
        GL.BindVertexArray(Handle);
    }

    public void UnBind()
    {
        GL.BindVertexArray(0);
    }

    ~VertexArray()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GL.DeleteVertexArray(Handle);
        GC.SuppressFinalize(this);
    }
}