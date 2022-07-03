using EngineSigma.Core.Rendering;

namespace EngineSigma.Core.Management;

public sealed class ResourceManager
{
    private readonly IGLWrapper _glWrapper;

    public ResourceManager(IGLWrapper glWrapper)
    {
        _glWrapper = glWrapper;
    }

    private readonly IDictionary<string, Texture> _textureCache = new Dictionary<string, Texture>();

    public Texture GetTexture(string textureName)
    {
        _textureCache.TryGetValue(textureName, out var value);
        {
            if (value is not null)
            {
                return value;
            }
        }
        value = Texture.Load(textureName, _glWrapper);
        _textureCache.Add(textureName, value);
        return value;
    }

    private readonly IDictionary<string, Image> _imageCache = new Dictionary<string, Image>();

    public Image GetImage(string imageName)
    {
        _imageCache.TryGetValue(imageName, out var value);
        {
            if (value is not null)
            {
                return value;
            }
        }
        value = new Image(imageName);
        _imageCache.Add(imageName, value);
        return value;
    }
}