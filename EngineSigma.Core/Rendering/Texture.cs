using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace EngineSigma.Core.Rendering;

public class Texture : IDisposable
{
    public int Handle { get; private set; }
    public int Width { get; set; }
    public int Height { get; set; }
    
    private bool _disposed;

    public Texture(int handle)
    {
        Handle = handle;
    }

    public Texture(int handle, int width, int height) : this(handle)
    {
        Width = width;
        Height = height;
    }
    
    public void Use()
    {
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }

    public static Texture Load(string textureName)
    {
        int handle = GL.GenTexture();
        
        GL.ActiveTexture(TextureUnit.Texture0);
        GL.BindTexture(TextureTarget.Texture2D, handle);
        
        using var image = new Bitmap(textureName);
        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);
        
        GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Nearest);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        
        //GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        
        return new Texture(handle, image.Width, image.Height);
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