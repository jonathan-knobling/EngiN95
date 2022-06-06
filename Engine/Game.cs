using EngineSigma.Engine.Rendering;
using EngineSigma.Engine.Rendering.Vertices;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using Image = OpenTK.Windowing.Common.Input.Image;

namespace EngineSigma.Engine;

public class Game: IDisposable
{
    private readonly Window _window;

    public Game()
    {
        var icon = new Rendering.Image(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Sprites\\stone.png");

        var settings = new NativeWindowSettings()
        {
            Title = "Engine Sigma",
            StartVisible = false,
            StartFocused = true,
            APIVersion = new Version(3,3),
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            WindowBorder = WindowBorder.Hidden,
            Size = new Vector2i(1920, 1080),
            Icon = new WindowIcon(new Image(icon.Width, icon.Height, icon.PixelData))
        };

        _window = new Window(settings);

        _window.UpdateFrame += Update;
    }


    private void Update(FrameEventArgs obj)
    {
        var rand = new Random();
        
        var x = rand.Next(1920);
        var y = rand.Next(1080);

        var vertices = new Vertex[4];

        vertices[0] = new Vertex(
            new Vector2(x, y+64),
            new Vector2(0f,1f),
            Color4.White);
        
        vertices[1] = new Vertex(
            new Vector2(x+64, y+64),
            new Vector2(1f,1f),
            Color4.White);
        
        vertices[2] = new Vertex(
            new Vector2(x+64, y),
            new Vector2(1f,0f),
            Color4.White);

        vertices[3] = new Vertex(
            new Vector2(x, y),
            new Vector2(0f,0f),
            Color4.White);

        var indices = new uint[6];
        var indexCount = 0;

        indices[indexCount++] = 0;
        indices[indexCount++] = 1;
        indices[indexCount++] = 2;
        indices[indexCount++] = 0;
        indices[indexCount++] = 2;
        indices[indexCount] = 3;

        var vb = new VertexBuffer(Vertex.VertexInfo, vertices.Length);
        vb.SetData(vertices, vertices.Length);

        var va = new VertexArray(vb);

        var ib = new IndexBuffer(indices.Length);
        ib.SetData(indices, indices.Length);

        var scale = rand.NextSingle() * 1.5f + 0.5f;
        
        var transform = Matrix4.CreateScale(scale);
        transform *= Matrix4.CreateRotationZ(rand.NextSingle() * 360f);

        var mesh = new Sprite(vb, va, ib, transform);
        _window.Renderer.AddMesh(mesh);
    }

    public void Run()
    {
        _window.Run();
    }

    public void Dispose()
    {
        _window.Dispose();
    }
}