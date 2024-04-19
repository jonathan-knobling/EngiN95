namespace EngiN95.Core.Rendering;

public interface IIndexBuffer : IDisposable
{
    public Handle Handle { get; }
    void Bind();
    void UnBind();
}