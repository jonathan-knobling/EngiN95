using EngiN95.Core;
using EngiN95.Core.Management;
using EngiN95.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngiN95;

#pragma warning disable CS8618

internal class GameImpl : Game
{
    public GameImpl(string windowTitle, int initialWindowWidth, int initialWindowHeight) : base(windowTitle, initialWindowWidth, initialWindowHeight)
    {
        vertices = new[]
        {
            new Vertex(new Vector3(600, 600, 0.0f), new Vector2(1.0f, 1.0f), Color4.White),
            new Vertex(new Vector3(600, 0, 0.0f), new Vector2(1.0f, 0.0f), Color4.White),
            new Vertex(new Vector3(0, 0, 0.0f), new Vector2(0.0f, 0.0f), Color4.White),
            new Vertex(new Vector3(0, 600, 0.0f), new Vector2(0.0f, 1.0f), Color4.White)
        };
        
        indices = new uint[]
        {
            0, 1, 3, 1, 2, 3
        };
    }

    private IShader shader;
    private Texture texture;
    private IndexBuffer indexBuffer;
    private VertexBuffer vertexBuffer;
    private VertexArray vertexArray;
    private readonly uint[] indices;
    private readonly Vertex[] vertices;

    protected override void Init()
    {
        
    }
    
    protected override void OnLoad()
    {
        var src = ShaderProgramSource.LoadFromFiles("Resources/Shaders/vertex.glsl", "Resources/Shaders/fragment.glsl");
        shader = new Shader(src);

        IGLWrapper glWrapper = new GLWrapper();
        
        vertexBuffer = new VertexBuffer(glWrapper);
        vertexBuffer.BufferData(vertices);
        vertexArray = new VertexArray(glWrapper);
        indexBuffer = new IndexBuffer(glWrapper, indices);

        var rm = new ResourceManager(glWrapper);
        
        texture = rm.GetTexture("Resources/Sprites/travisfish.png");
        texture.Use();

        shader.SetMatrix4("projection", 
            Matrix4.CreateOrthographic(GameWindow.Size.X, GameWindow.Size.Y, -1, 100));

        GL.Enable(EnableCap.Blend);
        GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
    }

    protected override void OnUpdate()
    {
        var movementDirection = InputHandler.MovementDirection;
        const float speed = 800f; // pixel/s
        //Console.WriteLine(movementDirection);
        Camera.Instance.Move(movementDirection * speed * Time.DeltaTime);
    }

    protected override void OnRender()
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        GL.ClearColor(Color4.CornflowerBlue);
        
        shader.Use();
        
        vertexBuffer.BufferData(vertices);

        vertexArray.Bind();
        indexBuffer.Bind();
        vertexBuffer.Bind();
        
            
        Matrix4.CreateScale(0.5f, out var scale);
        Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(180f * Time.TotalGameTime), out var rotation);
        Matrix4.CreateTranslation(400, 0, 0, out var translation);
        
        var transform = Matrix4.Identity;
        transform *= scale;
        transform *= rotation;
        transform *= translation;

        shader.SetMatrix4("transform", transform);

        shader.SetMatrix4("view", Camera.Instance.ToViewMatrix());

        GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedInt, 0);
    }
}