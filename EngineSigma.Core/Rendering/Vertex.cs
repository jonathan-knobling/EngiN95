using OpenTK.Mathematics;

namespace EngineSigma.Core.Rendering;

public struct Vertex
{
    public const int Size = ComponentAmount * sizeof(float);
    public const int ComponentAmount = 9;
    
    public Vector3 Position;
    public Vector2 TexCoords;
    public Color4 Color;

    public Vertex(Vector3 position, Vector2 texCoords, Color4 color)
    {
        Position = position;
        TexCoords = texCoords;
        Color = color;
    }

    public Vertex(Vector3 position, Vector2 texCoords)
    {
        Position = position;
        TexCoords = texCoords;
        Color = Color4.White;
    }
}