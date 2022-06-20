using EngineSigma.Core;
using EngineSigma.Core.Management;
using EngineSigma.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngineTest;

internal class GameImpl : Game
{
    public GameImpl(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
    {
        _vertices = new[]
        //{
        //    //Positions             //TextureCoords
        //     0.5f,   0.5f,  0.0f,   1.0f, 1.0f,      //top right 
        //     0.5f,  -0.5f,  0.0f,   1.0f, 0.0f,      //bottom right
        //    -0.5f,  -0.5f,  0.0f,   0.0f, 0.0f,      //bottom left 
        //    -0.5f,   0.5f,  0.0f,   0.0f, 1.0f       //top left 
        //};
        {
            new Vertex(new Vector3( 0.5f,  0.5f, 0.0f), new Vector2(1.0f, 1.0f)),
            new Vertex(new Vector3( 0.5f, -0.5f, 0.0f), new Vector2(1.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0.0f), new Vector2(0.0f, 0.0f)),
            new Vertex(new Vector3(-0.5f,  0.5f, 0.0f), new Vector2(0.0f, 1.0f))
        };
        

        _indices = new uint[]
        {
            0, 1, 3, 1, 2, 3
        };
    }
    
    private IShader _shader = null!;
    private Texture _texture = null!;
    
    //private int _vertexBufferObject;
    //private int _vertexArrayObject;
    //private int _indexBufferObject;
    private IndexBuffer _indexBuffer = null!;
    private DynamicVertexBuffer _vertexBuffer = null!;
    private VertexArray _vertexArray = null!;

    private readonly uint[] _indices;
    private readonly Vertex[] _vertices;

    protected override void Init()
    {
        
    }

    protected override void OnLoad()
    {
        var src = ShaderProgramSource.LoadFromFiles("Resources/Shaders/vertex.glsl", "Resources/Shaders/fragment.glsl");
        _shader = new Shader(src);
        
        _vertexBuffer = new DynamicVertexBuffer();
        _vertexBuffer.BufferData(_vertices);
        _vertexArray = new VertexArray();
        _indexBuffer = new IndexBuffer(_indices);
        
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
        
        _vertexArray.Bind();
        _indexBuffer.Bind();
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}