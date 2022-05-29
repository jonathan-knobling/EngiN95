using EngineSigma.Engine.Rendering;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Engine;

public class Game: IDisposable
{
    private readonly Window _window;

    public Game()
    {
        var settings = new NativeWindowSettings()
        {
            Title = "Engine Sigma",
            StartVisible = false,
            StartFocused = true,
            APIVersion = new Version(3,3),
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            WindowBorder = WindowBorder.Hidden,
            Size = new Vector2i(1920, 1080)
        };
        
        _window = new Window(settings);

        _window.UpdateFrame += Update;
    }

    private void Update(FrameEventArgs obj)
    {
        var rand = new Random();

        VertexPositionColor[] vertices = new VertexPositionColor[4];

        vertices[0] = new VertexPositionColor(new Vector2(rand.Next(1920), rand.Next(1080)), new Color4(rand.Next(1), rand.Next(120), rand.Next(120), 1));
        vertices[1] = new VertexPositionColor(new Vector2(rand.Next(1920), rand.Next(1080)), new Color4(rand.Next(1), rand.Next(120), rand.Next(1), 1));
        vertices[2] = new VertexPositionColor(new Vector2(rand.Next(1920), rand.Next(1080)), new Color4(rand.Next(1), rand.Next(120), rand.Next(120), 1));
        vertices[3] = new VertexPositionColor(new Vector2(rand.Next(1920), rand.Next(1080)), new Color4(rand.Next(120), rand.Next(1), rand.Next(120), 1));

        uint[] indices = new uint[6];
        int indexCount = 0;

        indices[indexCount++] = 0;
        indices[indexCount++] = 1;
        indices[indexCount++] = 2;
        indices[indexCount++] = 0;
        indices[indexCount++] = 2;
        indices[indexCount++] = 3;

        var vb = new VertexBuffer(VertexPositionColor.VertexInfo, vertices.Length);
        vb.SetData(vertices, vertices.Length);

        var va = new VertexArray(vb);

        var ib = new IndexBuffer(indices.Length);
        ib.SetData(indices, indices.Length);

        Mesh mesh = new Mesh(vb, va, ib);
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