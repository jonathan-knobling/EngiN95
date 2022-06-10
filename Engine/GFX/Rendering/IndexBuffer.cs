using OpenTK.Graphics.OpenGL;

namespace Engine.GFX.Rendering;

internal sealed class IndexBuffer : IDisposable
{
    public static readonly int MinIndexCount = 0;
    public static readonly int MaxIndexCount = 250_000;

    public readonly int IndexBufferHandle;
    public readonly int IndexCount;
    public readonly bool IsStatic;

    private bool _isDisposed;

    public IndexBuffer(int indexCount, bool isStatic = true)
    {
        _isDisposed = false;

        //Guard Statements
        if (indexCount < MinIndexCount || indexCount > MaxIndexCount)
            throw new ArgumentOutOfRangeException(nameof(indexCount));

        IndexCount = indexCount;
        IsStatic = isStatic;

        //Determine Buffer Usage Hint
        var hint = BufferUsageHint.StaticDraw;
        if (!IsStatic) hint = BufferUsageHint.StreamDraw;

        IndexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferHandle);
        GL.BufferData(BufferTarget.ElementArrayBuffer, IndexCount * sizeof(uint), IntPtr.Zero, hint);
    }

    public void Dispose()
    {
        if (_isDisposed) return;

        //Delete Index Buffer
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.DeleteBuffer(IndexBufferHandle);

        _isDisposed = true;
        GC.SuppressFinalize(this);
    }

    public void SetData(uint[] data, int count)
    {
        //Guard Statements
        if (data is null) throw new ArgumentNullException(nameof(data));
        if (data.Length < 1) throw new ArgumentOutOfRangeException();
        if (count < 1 || count > IndexCount || count > data.Length) throw new ArgumentOutOfRangeException();

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferHandle);
        GL.BufferSubData(BufferTarget.ElementArrayBuffer, IntPtr.Zero, count * sizeof(uint), data);
    }

    ~IndexBuffer()
    {
        Dispose();
    }
}