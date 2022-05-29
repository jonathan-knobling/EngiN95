using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine;

public class VertexBuffer: IDisposable
{
    public static readonly int MinVertexCount = 1;
    public static readonly int MaxVertexCount = 100_000;
    
    private bool _isDisposed;

    public readonly int VertexBufferHandle;
    public readonly VertexInfo VertexInfo;
    public readonly int VertexCount;
    public readonly bool IsStatic;
    
    public VertexBuffer(VertexInfo vertexInfo, int vertexCount, bool isStatic = true)
    {
        _isDisposed = false;
        
        //Guard Statements
        if (vertexCount < MinVertexCount || vertexCount > MaxVertexCount) throw new ArgumentOutOfRangeException(nameof(vertexCount));

        VertexInfo = vertexInfo;
        VertexCount = vertexCount;
        IsStatic = isStatic;

        //Determine Buffer Usage Hint
        BufferUsageHint hint = BufferUsageHint.StaticDraw;
        if (!isStatic) hint = BufferUsageHint.StreamDraw;

        //Generate Vertex Buffer
        VertexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexCount * vertexInfo.SizeInBytes, IntPtr.Zero, hint);
    }

    ~VertexBuffer()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_isDisposed) return;
        
        //Dispose of Vertex Buffer
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.DeleteBuffer(VertexBufferHandle);
        
        _isDisposed = true;
        GC.SuppressFinalize(this);
    }

    public void SetData<T> (T[] data, int count) where T : struct
    {
        //Guard Statements
        if (VertexInfo.Type != typeof(T)) throw new ArgumentException("Type 'T' does not match the vertex type of the VertexBuffer.");
        if (data is null) throw new ArgumentNullException(nameof(data));
        if (data.Length < 1) throw new ArgumentOutOfRangeException(nameof(data));
        if (count <= 0 || count > VertexCount || count > data.Length) throw new ArgumentOutOfRangeException(nameof(count));

        //Send Data to VertexBuffer
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, count * VertexInfo.SizeInBytes, data);
    }
}