namespace EngineSigma.Core.Rendering;

public interface IIndexBuffer : IDisposable
{
    public int Handle { get; }
    void Bind();
    void UnBind();
}