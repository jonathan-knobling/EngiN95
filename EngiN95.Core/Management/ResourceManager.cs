using EngiN95.Core.Rendering;

namespace EngiN95.Core.Management;

public sealed class ResourceManager
{
    public static ResourceManager Instance { get; } = new();
    private readonly IGLWrapper _glWrapper;
    private ResourceManager()
    {
        _glWrapper = new GLWrapper();
    }
    
    private readonly Dictionary<string, Texture> _textureCache = new();
    
    public Texture GetTexture(string textureName)
    {
        _textureCache.TryGetValue(textureName, out var texture);
        {
            if (texture is not null)
            {
                return texture;
            }
        }
        texture = Texture.Load(textureName, _glWrapper);
        _textureCache.Add(textureName, texture);
        return texture;
    }
}