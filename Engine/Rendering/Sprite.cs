using OpenTK.Mathematics;

namespace EngineSigma.Engine.Rendering;

public struct Sprite
{
    public VertexBuffer VertexBuffer;
    public VertexArray VertexArray;
    public IndexBuffer IndexBuffer;
    public Matrix4 Transform;
    public readonly string ID;
    public Sprite(VertexBuffer vb, VertexArray va, IndexBuffer ib, Matrix4 transform)
    {
        VertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        Transform = transform;
        ID = Guid.NewGuid().ToString();
    }
    
    public Sprite(VertexBuffer vb, VertexArray va, IndexBuffer ib)
    {
        VertexBuffer = vb;
        VertexArray = va;
        IndexBuffer = ib;
        Transform = Matrix4.Identity;
        ID = Guid.NewGuid().ToString();
    }
}