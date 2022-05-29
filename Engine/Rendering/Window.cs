using EngineSigma.Engine.Rendering.shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace EngineSigma.Engine.Rendering;

public class Window: GameWindow
{
    public readonly Renderer Renderer;
    
    private Shader _shader;
    
#pragma warning disable CS8618
    public Window(NativeWindowSettings settings) : base(GameWindowSettings.Default, settings)
    {
        CenterWindow();
        IsVisible = true;
        Renderer = new Renderer();
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

        //Create Shader
        _shader = new Shader("C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\Engine\\Rendering\\Shaders\\shader.vert", 
                            "C:\\Users\\happy\\Desktop\\GitHub\\EngineSigma\\Engine\\Rendering\\Shaders\\shader.frag");

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
        _shader.Dispose();
        
        base.OnUnload();
    }

    protected override void OnRenderFrame(FrameEventArgs args)
    {
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        _shader.Use();
        
        Renderer.Render();

        Context.SwapBuffers();
        base.OnRenderFrame(args);
    }
}