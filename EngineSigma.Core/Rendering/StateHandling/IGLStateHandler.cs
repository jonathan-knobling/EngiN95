namespace EngineSigma.Core.Rendering;

public interface IGLStateHandler
{
    void UseTexture(Handle handle);
    void UseVertexArray(Handle handle);
    void UseVertexBuffer(Handle handle);
    void UseIndexBuffer(Handle handle);
}