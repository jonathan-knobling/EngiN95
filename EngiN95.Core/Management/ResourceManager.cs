using EngiN95.Core.Rendering;

namespace EngiN95.Core.Management;

public sealed class ResourceManager
{
    private readonly IGLWrapper _glWrapper;

    public ResourceManager(IGLWrapper glWrapper)
    {
        _glWrapper = glWrapper;
    }

    private readonly Dictionary<string, Texture> _textureCache = new();

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
}