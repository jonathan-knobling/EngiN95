namespace EngineSigma.Core.Rendering;

public interface IVertexArray : IDisposable
{
    public Handle Handle { get; }
    void Bind();
    void UnBind();
}