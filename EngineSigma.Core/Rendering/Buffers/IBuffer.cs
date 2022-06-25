namespace EngineSigma.Core.Rendering;

public interface IBuffer : IDisposable
{
    int Handle { get; }
    void Bind();
    void UnBind();
}