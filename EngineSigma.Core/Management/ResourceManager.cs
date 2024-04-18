using EngineSigma.Core.Rendering;
using Image = EngineSigma.Core.Rendering.Image;

namespace EngineSigma.Core.Management;

public sealed class ResourceManager
{
    private readonly IGLWrapper _glWrapper;

    public ResourceManager(IGLWrapper glWrapper)
    {
        _glWrapper = glWrapper;
    }

    private readonly Dictionary<string, Texture> _textureCache = new();
    private readonly Dictionary<string, Image> _imageCache = new();

    public Texture GetTexture(string texturePath)
    {
        _textureCache.TryGetValue(texturePath, out var texture);
        {
            if (texture is not null)
            {
                return texture;
            }
        }
        texture = Texture.Load(texturePath, _glWrapper);
        _textureCache.Add(texturePath, texture);
        return texture;
    }

    public Image GetImage(string imagePath)
    {
        _imageCache.TryGetValue(imagePath, out var image);
        {
            if (image is not null)
            {
                return image;
            }
        }
        image = new Image(imagePath);
        _imageCache.Add(imagePath, image);
        return image;
    }
}