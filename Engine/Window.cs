using EngineSigma.Engine.shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Engine;

public class Window: GameWindow
{
    private Shader _shader;
    private VertexBuffer _vertexBuffer;
    private IndexBuffer _indexBuffer;
    private VertexArray _vertexArray;

    private double _time;
    
#pragma warning disable CS8618
    public Window() : base(GameWindowSettings.Default, NativeWindowSettings.Default)
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
        //Background Color
        GL.ClearColor(Color4.Black);

        //Rectangle Dimensions
        float x = 384f;
        float y = 400f;
        float w = 512f;
        float h = 256f;

        //float[] vertices =
        //{
        //    x, y+h, 1f, 1f, 1f, 1f,
        //    x+w, y+h, 1f, 1f, 0f, 1f,
        //    x+w, y, 0f, 1f, 1f, 1f,
        //    x, y, 1f, 0f, 1f, 1f,
        //};
        
        //Vertex Array
        VertexPositionColor[] vertices =
        {
            new VertexPositionColor(new Vector2(x, y + h), new Color4(1f, 0f, 0f, 1f)),
            new VertexPositionColor(new Vector2(x+w, y+h), new Color4(0f, 1f, 0f, 1f)),
            new VertexPositionColor(new Vector2(x+w, y), new Color4(0f, 0f, 1f, 1f)),
            new VertexPositionColor(new Vector2(x, y), new Color4(1f, 1f, 0f, 1f)),
        };

        //Index Buffer
        uint[] indices =
        {
            0, 1, 2, 0, 2, 3
        };
        
        //Create Vertex Buffer
        _vertexBuffer = new VertexBuffer(VertexPositionColor.VertexInfo, vertices.Length);
        //Set Vertex Buffer Data
        _vertexBuffer.SetData(vertices, vertices.Length);
        
        //Create Index Buffer
        _indexBuffer = new IndexBuffer(indices.Length);
        //Set Index Buffer Data
        _indexBuffer.SetData(indices, indices.Length);
        
        //Create Vertex Array
        _vertexArray = new VertexArray(_vertexBuffer);

        //Create Shader
        _shader = new Shader("C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\engine\\shaders\\shader.vert", 
                            "C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\engine\\shaders\\shader.frag");

        //Get Viewport Size
        int[] viewport = new int[4];
        GL.GetInteger(GetPName.Viewport, viewport);
        
        //Set Viewport Size in Shader
        _shader.Use();
        int vpsLocation = GL.GetUniformLocation(_shader.Handle, "viewPortSize");
        GL.Uniform2(vpsLocation, (float) viewport[2], viewport[3]);
        
        base.OnLoad();
    }

    protected override void OnUnload()
    {
        _vertexArray.Dispose();
        _indexBuffer.Dispose();
        _vertexBuffer.Dispose();
        
        _shader.Dispose();
        
        base.OnUnload();
    }

    protected override void OnUpdateFrame(FrameEventArgs args)
    {
        _time += args.Time;
        
        //float[] vertices =
        //{
        //    -0.9f, -0.9f, 0f, (float) Math.Cos(_time), 1f, 1f, 1f,
        //    -0.9f, 0.9f, 0f, (float) Math.Sin(_time), (float) Math.Cos(_time), 0f, 1f,
        //    0.9f, 0.9f, 0f, 0f, (float) Math.Sin(_time), (float) Math.Cos(_time), 1f,
        //    0.9f, -0.9f, 0f, (float) Math.Cos(_time), 0f, (float) Math.Sin(_time), 1f,
        //};
        //
        //GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferHandle);
        //GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        base.OnUpdateFrame(args);
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        _shader.Use();

        GL.BindVertexArray(_vertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBuffer.IndexBufferHandle);
        
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);

        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }
}