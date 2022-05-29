using EngineSigma.Engine.shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Engine;

public class Window: GameWindow
{
    private Shader? _shader;
    
    private int _vertexBufferHandle;
    private int _vertexArrayHandle;
    private int _indexBufferHandle;

    private double _time;
    
    public Window() 
        : base(GameWindowSettings.Default, NativeWindowSettings.Default)
    {
        _time = 0;
        CenterWindow(new Vector2i(1280,768));
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);

        base.OnResize(e);
    }

    protected override void OnLoad()
    {
        GL.ClearColor(Color4.Black);

        float[] vertices =
        {
            -0.9f, -0.9f, 0f, 1f, 1f, 1f, 1f,
            -0.9f, 0.9f, 0f, 1f, 1f, 0f, 1f,
            0.9f, 0.9f, 0f, 0f, 1f, 1f, 1f,
            0.9f, -0.9f, 0f, 1f, 0f, 1f, 1f,
        };

        int[] indices =
        {
            0, 1, 2, 0, 2, 3
        };

        _vertexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        _indexBufferHandle = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferHandle);
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
        
        _vertexArrayHandle = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayHandle);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 7 * sizeof(float), 0);
        GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, 7 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(0);
        GL.EnableVertexAttribArray(1);
        
        //Unbind Vertex Array
        GL.BindVertexArray(0);
        
        _shader = new Shader("C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\engine\\shaders\\shader.vert", 
                            "C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\engine\\shaders\\shader.frag");

        base.OnLoad();
    }

    protected override void OnUnload()
    {
        GL.BindVertexArray(0);
        GL.DeleteVertexArray(_vertexArrayHandle);

        GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        GL.DeleteBuffer(_indexBufferHandle);
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        GL.DeleteBuffer(_vertexBufferHandle);
        
        _shader!.Dispose();
        
        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        _time += args.Time;
        
        float[] vertices =
        {
            -0.9f, -0.9f, 0f, (float) Math.Cos(_time), 1f, 1f, 1f,
            -0.9f, 0.9f, 0f, (float) Math.Sin(_time), (float) Math.Cos(_time), 0f, 1f,
            0.9f, 0.9f, 0f, 0f, (float) Math.Sin(_time), (float) Math.Cos(_time), 1f,
            0.9f, -0.9f, 0f, (float) Math.Cos(_time), 0f, (float) Math.Sin(_time), 1f,
        };
        
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        _shader!.Use();

        GL.BindVertexArray(_vertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferHandle);
        
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }
}