using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using Rectangle = System.Drawing.Rectangle;

namespace EngineSigma.Core.Rendering;

public class Texture : IDisposable
{
    public Handle Handle { get; }
    public int Width { get; }
    public int Height { get; }
    
    private bool _disposed;
    private readonly IGLWrapper _glWrapper;

    public Texture(int handle, IGLWrapper glWrapper)
    {
        Handle = handle;
        _glWrapper = glWrapper;
    }

    public Texture(int handle, int width, int height, IGLWrapper glWrapper) : this(handle, glWrapper)
    {
        Width = width;
        Height = height;
    }
    
    public void Use()
    {
        _glWrapper.ActiveTexture(TextureUnit.Texture0);
        _glWrapper.BindTexture(TextureTarget.Texture2D, Handle);
    }

    public static Texture Load(string textureName, IGLWrapper glWrapper)
    {
        int handle = glWrapper.GenTexture();

        glWrapper.ActiveTexture(TextureUnit.Texture0);
        glWrapper.BindTexture(TextureTarget.Texture2D, handle);

#pragma warning disable CA1416 
        //only works on windows
        
        using var image = new Bitmap(textureName);
        image.RotateFlip(RotateFlipType.RotateNoneFlipY);
        var data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly,
            PixelFormat.Format32bppArgb);

        glWrapper.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, image.Width, image.Height, 0,
            OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
        
        glWrapper.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter,
            (int) TextureMinFilter.Nearest);
        glWrapper.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
            (int) TextureMinFilter.Nearest);
        glWrapper.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS,
            (int) TextureWrapMode.Repeat);
        glWrapper.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT,
            (int) TextureWrapMode.Repeat);

        glWrapper.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        
        glWrapper.BindTexture(TextureTarget.Texture2D, 0);

        return new Texture(handle, image.Width, image.Height, glWrapper);
        
#pragma warning restore CA1416
    }

    ~Texture()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        _glWrapper.DeleteTexture(Handle);
        GC.SuppressFinalize(this);
    }
}