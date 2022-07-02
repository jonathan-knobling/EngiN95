namespace EngineSigma.Core.Rendering;

public interface IVertexBuffer : IDisposable
{
    public Handle Handle { get; }
    void BufferData(Vertex[] data);
    void Bind();
    void UnBind();
}