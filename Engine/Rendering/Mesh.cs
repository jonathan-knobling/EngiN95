
namespace EngineSigma.Engine.Rendering;

public struct Mesh
{
    public VertexBuffer VertexBuffer;
    public VertexArray VertexArray;
    public IndexBuffer IndexBuffer;
    public string ID;

    public Mesh(VertexBuffer vb, VertexArray va, IndexBuffer ib)
    {
        VertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        ID = Guid.NewGuid().ToString();
    }
}