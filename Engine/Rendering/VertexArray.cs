using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine.Rendering;

public class VertexArray: IDisposable
{
    private bool _isDisposed;

    public readonly int VertexArrayHandle;

    public VertexArray(VertexBuffer vertexBuffer)
    {
        _isDisposed = false;
        
        //Guard Statements
        if (vertexBuffer is null) throw new ArgumentNullException(nameof(vertexBuffer));

        //Get Size of Vertex from Vertex Buffer
        int vertexSizeInBytes = vertexBuffer.VertexInfo.SizeInBytes;
        
        //Generate Vertex Array
        VertexArrayHandle = GL.GenVertexArray();
        
        //Bind Vertex Array and Vertex Buffer
        GL.BindVertexArray(VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBuffer.VertexBufferHandle);

        //Get Vertex Attributes
        VertexAttribute[] attributes = vertexBuffer.VertexInfo.VertexAttributes;

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

    ~VertexArray()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if(_isDisposed) return;

        GL.BindVertexArray(0);
        GL.DeleteVertexArray(VertexArrayHandle);
        
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}