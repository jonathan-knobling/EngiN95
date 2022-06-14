using EngineSigma.Core;
using EngineSigma.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngineTest.Implementations;

internal class TextureTest : Game
{
    public TextureTest(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
    {
        
    }

    private Shader _shader = null!;
    
    private int _vertexBufferObject;
    private int _vertexArrayObject;

    protected override void Init()
    {
        
    }

    protected override void OnLoad()
    {
        ShaderProgramSource src = ShaderProgramSource.LoadFromFiles("Resources/Shaders/vertex.glsl", "Resources/Shaders/fragment.glsl");
        _shader = new Shader(src);
        
        float[] vertices =
        {   //Position x,y,z     //Colors
            -0.5f, -0.5f, 0.0f,  1.0f, 0.0f, 0.0f,
            0.5f, -0.5f, 0.0f,  0.0f, 1.0f, 0.0f,
            0.0f,  0.5f, 0.0f,  0.0f, 0.0f, 1.0f,
        };
        
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);
    }

    protected override void OnUpdate()
    {
        
    }

    protected override void OnRender()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(Color4.CornflowerBlue);
        
        _shader.Use();
        
        GL.BindVertexArray(_vertexArrayObject);
        GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
    }
}