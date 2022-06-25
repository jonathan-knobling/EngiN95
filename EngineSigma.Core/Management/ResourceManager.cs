using EngineSigma.Core.Rendering;

namespace EngineSigma.Core.Management;

public static class ResourceManager
{
    private static readonly IDictionary<string, Texture> TextureCache = new Dictionary<string, Texture>();

    public static Texture LoadTexture(string textureName)
    {
        TextureCache.TryGetValue(textureName, out var value);
        {
            if (value is not null)
            {
                return value;
            }
        }
        value = Texture.Load(textureName);
        TextureCache.Add(textureName, value);
        return value;
    }
}