namespace EngineSigma.Core.Rendering;

public interface IVertexBuffer : IDisposable
{
    public int Handle { get; }
    void BufferData(Vertex[] data);
    void Bind();
    void UnBind();
}