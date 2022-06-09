using EngineSigma.GFX.Rendering;

namespace EngineSigma.GFX;

public struct Sprite
{
    internal VertexBuffer VertexBuffer;
    internal VertexArray VertexArray;
    internal IndexBuffer IndexBuffer;
    public readonly string ID;

    internal Sprite(VertexBuffer vb, VertexArray va, IndexBuffer ib)
    {
        VertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        ID = Guid.NewGuid().ToString();
    }
}