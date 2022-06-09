using OpenTK.Graphics.OpenGL;

namespace EngineSigma.GFX.Rendering;

internal class VertexArray : IDisposable
{
    public readonly int VertexArrayHandle;
    private bool _isDisposed;

    public VertexArray(VertexBuffer vertexBuffer)
    {
        _isDisposed = false;

        //Guard Statements
        if (vertexBuffer is null) throw new ArgumentNullException(nameof(vertexBuffer));

        //Get Size of Vertex from Vertex Buffer
        var vertexSizeInBytes = vertexBuffer.VertexInfo.SizeInBytes;

        //Generate Vertex Array
        VertexArrayHandle = GL.GenVertexArray();

        //Bind Vertex Array and Vertex Buffer
        GL.BindVertexArray(VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer.VertexBufferHandle);

        //Get Vertex Attributes
        var attributes = vertexBuffer.VertexInfo.VertexAttributes;

        //Apply Vertex Attributes to Pointers
        foreach (var attribute in attributes)
        {
            GL.VertexAttribPointer(
                attribute.Index,
                attribute.ComponentCount,
                VertexAttribPointerType.Float,
                false,
                vertexSizeInBytes,
                attribute.Offset);

            GL.EnableVertexAttribArray(attribute.Index);
        }

        //Unbind Vertex Array
        GL.BindVertexArray(0);
    }

    public void Dispose()
    {
        if (_isDisposed) return;

        GL.BindVertexArray(0);
        GL.DeleteVertexArray(VertexArrayHandle);

        _isDisposed = true;
        GC.SuppressFinalize(this);
    }

    ~VertexArray()
    {
        Dispose();
    }
}