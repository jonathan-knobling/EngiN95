using EngineSigma.Core;
using EngineSigma.Core.Management;
using EngineSigma.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngineTest.Implementations;

internal class TextureTest : Game
{
    public TextureTest(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
    {
        _vertices = new [] 
        {
            //Positions             //TextureCoords
             0.5f,   0.5f,  0.0f,   1.0f, 1.0f,      //top right 
             0.5f,  -0.5f,  0.0f,   1.0f, 0.0f,      //bottom right
            -0.5f,  -0.5f,  0.0f,   0.0f, 0.0f,      //bottom left 
            -0.5f,   0.5f,  0.0f,   0.0f, 1.0f       //top left 
        };

        _indices = new uint[] 
        {
            0, 1, 3,
            1, 2, 3
        };
    }
    
    private Shader _shader = null!;
    private Texture _texture = null!;
    
    private int _vertexBufferObject;
    private int _vertexArrayObject;
    private int _indexBufferObject;
    
    private readonly uint[] _indices;
    private readonly float[] _vertices;

    protected override void Init()
    {
        
    }

    protected override void OnLoad()
    {
        var src = ShaderProgramSource.LoadFromFiles("Resources/Shaders/vertex.glsl", "Resources/Shaders/fragment.glsl");
        _shader = new Shader(src);
        
        _vertexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
        GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

        _vertexArrayObject = GL.GenVertexArray();
        GL.BindVertexArray(_vertexArrayObject);
        
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);
        GL.EnableVertexAttribArray(0);
        
        GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));
        GL.EnableVertexAttribArray(1);

        _indexBufferObject = GL.GenBuffer();
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferObject);
        GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Length * sizeof(uint), _indices, BufferUsageHint.StaticDraw);

        _texture = ResourceManager.LoadTexture("Resources/Sprites/wall.jpg");
        _texture.Use();
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
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}