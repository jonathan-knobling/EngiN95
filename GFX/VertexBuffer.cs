using EngineSigma.GFX.Vertices;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.GFX;

internal class VertexBuffer : IDisposable
{
    private const int MinVertexCount = 0;
    private const int MaxVertexCount = 100_000;
    private readonly int _vertexCount;

    public readonly int VertexBufferHandle;
    public readonly VertexInfo VertexInfo;

    private bool _isDisposed;

    public VertexBuffer(VertexInfo vertexInfo, int vertexCount, bool isStatic = true)
    {
        _isDisposed = false;

        //Guard Statements
        if (vertexCount is < MinVertexCount or > MaxVertexCount)
            throw new ArgumentOutOfRangeException(nameof(vertexCount));

        VertexInfo = vertexInfo;
        _vertexCount = vertexCount;

        //Determine Buffer Usage Hint
        var hint = BufferUsageHint.StaticDraw;
        if (!isStatic) hint = BufferUsageHint.StreamDraw;

        //Generate Vertex Buffer
        VertexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertexCount * vertexInfo.SizeInBytes, IntPtr.Zero, hint);
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

    public void SetData<T>(T[] data, int count) where T : struct
    {
        //Guard Statements
        if (VertexInfo.Type != typeof(T))
            throw new ArgumentException("Type 'T' does not match the vertex type of the VertexBuffer.");
        if (data is null) throw new ArgumentNullException(nameof(data));
        if (data.Length < 1) throw new ArgumentOutOfRangeException(nameof(data));
        if (count <= 0 || count > _vertexCount || count > data.Length)
            throw new ArgumentOutOfRangeException(nameof(count));

        //Send Data to VertexBuffer
        GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferHandle);
        GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, count * VertexInfo.SizeInBytes, data);
    }

    ~VertexBuffer()
    {
        Dispose();
    }
}