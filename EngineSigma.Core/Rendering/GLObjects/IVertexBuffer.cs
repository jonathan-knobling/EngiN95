namespace EngineSigma.Core.Rendering;

public interface IVertexBuffer : IDisposable
{
    public int Handle { get; }
    void Bind();
    void UnBind();
}