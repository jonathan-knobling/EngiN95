using EngineSigma.Core.Management.Textures;
using EngineSigma.Core.Rendering;

namespace EngineSigma.Core.Management;

public static class ResourceManager
{
    private static IDictionary<string, Texture> _textureCache = new Dictionary<string, Texture>();

    public static Texture LoadTexture(string textureName)
    {
        _textureCache.TryGetValue(textureName, out var value);
        {
            if (value is not null)
            {
                return value;
            }
        }
        value = TextureFactory.Load(textureName);
        _textureCache.Add(textureName, value);
        return value;
    }
}