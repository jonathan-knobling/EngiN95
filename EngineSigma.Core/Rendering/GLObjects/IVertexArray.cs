namespace EngineSigma.Core.Rendering;

public interface IVertexArray : IDisposable
{
    public int Handle { get; }
    void Bind();
    void UnBind();
}