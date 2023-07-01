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
        {
            new Vertex(new Vector3(600, 600, 0.0f), new Vector2(1.0f, 1.0f), Color4.DarkBlue),
            new Vertex(new Vector3(600, 0, 0.0f), new Vector2(1.0f, 0.0f), Color4.Transparent),
            new Vertex(new Vector3(0, 0, 0.0f), new Vector2(0.0f, 0.0f), Color4.Fuchsia),
            new Vertex(new Vector3(0, 600, 0.0f), new Vector2(0.0f, 1.0f), Color4.Lime)
        };
        
        _indices = new uint[]
        {
            0, 1, 3, 1, 2, 3
        };
    }

    private IShader _shader = null!;
    private Texture _texture = null! ;
    private IndexBuffer _indexBuffer = null!;
    private VertexBuffer _vertexBuffer = null!;
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

        IGLWrapper glWrapper = new GLWrapper();
        
        _vertexBuffer = new VertexBuffer(glWrapper);
        _vertexBuffer.BufferData(_vertices);
        _vertexArray = new VertexArray(glWrapper);
        _indexBuffer = new IndexBuffer(glWrapper, _indices);

        var rm = new ResourceManager(glWrapper);
        
        _texture = rm.GetTexture("Resources/Sprites/wall.jpg");
        _texture.Use();

        _shader.SetMatrix4("projection", 
            Matrix4.CreateOrthographic(GameWindow.Size.X, GameWindow.Size.Y, -1, 100));
    }

    protected override void OnUpdate()
    {
        var movementDirection = InputHandler.MovementDirection;
        const float speed = 800f; // pixel/s
        Console.WriteLine(movementDirection);
        Camera.Instance.Move(movementDirection * speed * Time.DeltaTime);
    }

    protected override void OnRender()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(Color4.CornflowerBlue);
        
        _shader.Use();
        
        _vertexBuffer.BufferData(_vertices);
        
        _vertexArray.Bind();
        _indexBuffer.Bind();
        _vertexBuffer.Bind();
        
        Matrix4.CreateScale(0.5f, out var scale);
        Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(180f * Time.TotalGameTime), out var rotation);
        Matrix4.CreateTranslation(400, 0, 0, out var translation);

        var transform = Matrix4.Identity;
        transform *= scale;
        transform *= rotation;
        transform *= translation;

        _shader.SetMatrix4("transform", transform);
        
        _shader.SetMatrix4("view", Camera.Instance.ToViewMatrix());
        
        GL.DrawElements(PrimitiveType.Triangles, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}