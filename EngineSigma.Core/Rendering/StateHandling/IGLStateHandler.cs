namespace EngineSigma.Core.Rendering;

public interface IGLStateHandler
{
    Handle ActiveTexture { get; protected set; }
    Handle ActiveVertexArray { get; protected set; }
    Handle ActiveVertexBuffer { get; protected set; }
    Handle ActiveIndexBuffer { get; protected set; }
    void UseTexture(Handle handle);
    void UseVertexArray(Handle handle);
    void UseVertexBuffer(Handle handle);
    void UseIndexBuffer(Handle handle);
}