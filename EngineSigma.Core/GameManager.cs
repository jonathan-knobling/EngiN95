using EngineSigma.Core.ECS;
using EngineSigma.Core.ECS.Components;
using EngineSigma.Core.GFX;
using EngineSigma.Core.GFX.Rendering;
using EngineSigma.Core.GFX.Vertices;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Common.Input;
using OpenTK.Windowing.Desktop;
using Image = EngineSigma.Core.GFX.Image;

namespace EngineSigma.Core;

public class GameManager : IDisposable
{
    private readonly Window _window;

    public GameManager()
    {
        var icon = new Image(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\Sprites\\stone.png");

        var settings = new NativeWindowSettings
        {
            Title = "EngineSigma.Core Sigma",
            StartVisible = false,
            StartFocused = true,
            APIVersion = new Version(3, 3),
            API = ContextAPI.OpenGL,
            Profile = ContextProfile.Core,
            WindowBorder = WindowBorder.Hidden,
            Size = new Vector2i(Window.Width, Window.Height),
            Icon = new WindowIcon(new OpenTK.Windowing.Common.Input.Image(icon.Width, icon.Height, icon.PixelData))
        };

        _window = new Window(settings);

        _window.UpdateFrame += Update;
        _window.RenderFrame += Render;
    }

    /// <summary>
    /// Gets called right after the window is updated
    /// base.Update updates the EntityManager
    /// </summary>
    /// <param name="args">Includes the Time since the last update</param>
    protected virtual void Update(FrameEventArgs args)
    {
        var rand = new Random();

        int x = rand.Next(1920);
        int y = rand.Next(1080);

        var vertices = new Vertex[4];

        vertices[0] = new Vertex(
            new Vector2(x, y + 64),
            new Vector2(0f, 1f),
            Color4.White);

        vertices[1] = new Vertex(
            new Vector2(x + 64, y + 64),
            new Vector2(1f, 1f),
            Color4.White);

        vertices[2] = new Vertex(
            new Vector2(x + 64, y),
            new Vector2(1f, 0f),
            Color4.White);

        vertices[3] = new Vertex(
            new Vector2(x, y),
            new Vector2(0f, 0f),
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

        float scale = rand.NextSingle() * 1.5f + 0.5f;

        var transform = Matrix4.CreateScale(scale);
        transform *= Matrix4.CreateRotationZ(rand.NextSingle() * 360f);

        var sprite = new Sprite(vb, va, ib);

        var entity = Entity.Instantiate(new SpriteRenderer());
        entity.GetComponent<SpriteRenderer>()!.Sprite = sprite;
        entity.Transform.SetTransformationWithMatrix(transform);
    }

    /// <summary>
    ///     Gets called directly after the window was rendered
    /// </summary>
    /// <param name="args">Includes the Time since the last Frame was rendered</param>
    protected virtual void Render(FrameEventArgs args)
    {
        
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