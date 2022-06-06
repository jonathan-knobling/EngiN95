namespace EngineSigma.Engine.Rendering.Vertices;

public readonly struct VertexAttribute
{
    private readonly string _name;
    public readonly int Index;
    public readonly int ComponentCount;
    //public readonly VertexAttribPointerType Type;
    public readonly int Offset;

    public VertexAttribute(string name, int index, int componentCount,  /*VertexAttribPointerType type,*/ int offset)
    {
        _name = name;
        Index = index;
        ComponentCount = componentCount;
        //Type = type;
        Offset = offset;
    }
}