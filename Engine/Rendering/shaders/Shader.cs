using System.Text;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine.Rendering.shaders;

public sealed class Shader
{
    public int Handle { get; }

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexShaderSource;

        using (var reader = new StreamReader(vertexPath, Encoding.UTF8))
        {
            vertexShaderSource = reader.ReadToEnd();
        }

        string fragmentShaderSource;

        using (var reader = new StreamReader(fragmentPath, Encoding.UTF8))
        {
            fragmentShaderSource = reader.ReadToEnd();
        }
        
        var vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        
        GL.CompileShader(vertexShader);

        var infoLogVert = GL.GetShaderInfoLog(vertexShader);
        if (infoLogVert != string.Empty)
            Console.WriteLine(infoLogVert);

        GL.CompileShader(fragmentShader);

        var infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
        if (infoLogFrag != string.Empty)
            Console.WriteLine(infoLogFrag);
        
        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, vertexShader);
        GL.AttachShader(Handle, fragmentShader);

        GL.LinkProgram(Handle);
        
        GL.DetachShader(Handle, vertexShader);
        GL.DetachShader(Handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    private bool _disposedValue;

    ~Shader()
    {
        Dispose();
    }
    
    public void Dispose()
    {
        if (_disposedValue) return;
        
        GL.DeleteProgram(Handle);

        _disposedValue = true;

        GC.SuppressFinalize(this);
    }
}