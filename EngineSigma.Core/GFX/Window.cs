using EngineSigma.Core.ECS;
using EngineSigma.Core.GFX.Rendering;
using EngineSigma.Core.GFX.Shaders;
using EngineSigma.Core.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Core.GFX;

internal class Window : GameWindow
{
    public const int Width = 1920;
    public const int Height = 1080;

    public readonly Renderer Renderer;

    private Shader _shader = null!;

    public Window(NativeWindowSettings settings) : base(GameWindowSettings.Default, settings)
    {
        CenterWindow();
        IsVisible = true;
        Renderer = new Renderer();

        Input.KeyboardState = KeyboardState;
        Input.MouseState = MouseState;
    }

    protected override void OnLoad()
    {
        //Set Background Color
        GL.ClearColor(Color4.Gray);

        //Enable Depth Testing
        GL.Enable(EnableCap.DepthTest);

        //Get root Directory
        string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

        //Create Shader
        _shader = new Shader($"{rootDirectory}\\GFX\\Shaders\\shader.vert",
            $"{rootDirectory}\\GFX\\Shaders\\shader.frag");

        //Get Viewport Size
        var viewport = new int[4];
        GL.GetInteger(GetPName.Viewport, viewport);

        //Set Viewport Size in Shader
        _shader.Use();
        int vpsLocation = GL.GetUniformLocation(_shader.Handle, "viewPortSize");
        GL.Uniform2(vpsLocation, (float) viewport[2], viewport[3]);

        //Load Texture
        var texture = Texture.LoadFromFile("Resources/Sprites/grass.png");
        texture.Use(TextureUnit.Texture0);

        base.OnLoad();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        //Clear Depth and Color Buffer
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        //Use Shader
        _shader.Use();
        
        //Add Renderables to Renderer
        EntityManager.OnRender(Renderer);

        //Render sprites
        Renderer.Render(_shader);

        //Swap Buffers
        Context.SwapBuffers();
        
        base.OnRenderFrame(args);
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        GL.Viewport(0, 0, e.Width, e.Height);

        base.OnResize(e);
    }

    protected override void OnUnload()
    {
        _shader.Dispose();

        base.OnUnload();
    }
}