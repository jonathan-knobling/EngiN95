using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class Texture : IDisposable
{
    public int Handle { get; private set; }

    private bool _disposed;

    public Texture(int handle)
    {
        Handle = handle;
        _disposed = false;
    }

    public void Use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    ~Texture()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        GL.DeleteTexture(Handle);
        GC.SuppressFinalize(this);
    }
}