namespace EngineSigma.Core.GFX.Vertices;

internal sealed class VertexInfo
{
    public readonly int SizeInBytes;
    public readonly Type Type;
    public readonly VertexAttribute[] VertexAttributes;

    public VertexInfo(Type type, params VertexAttribute[] attributes)
    {
        Type = type;
        SizeInBytes = 0;
        VertexAttributes = attributes;

        foreach (var attribute in VertexAttributes) SizeInBytes += attribute.ComponentCount * sizeof(float);
    }
}