using OpenTK.Mathematics;

namespace EngineSigma.GFX;

internal struct Sprite
{
    public VertexBuffer _vertexBuffer;
    public VertexArray VertexArray;
    public IndexBuffer IndexBuffer;
    public Matrix4 Transform;
    public readonly string ID;

    public Sprite(VertexBuffer vb, VertexArray va, IndexBuffer ib, Matrix4 transform)
    {
        _vertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        Transform = transform;
        ID = Guid.NewGuid().ToString();
    }

    public Sprite(VertexBuffer vb, VertexArray va, IndexBuffer ib)
    {
        _vertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        Transform = Matrix4.Identity;
        ID = Guid.NewGuid().ToString();
    }
}