using System.Text;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Engine.shaders;

public sealed class Shader
{
    public int Handle { get; }

    public Shader(string vertexPath, string fragmentPath)
    {
        string vertexShaderSource;

        using (StreamReader reader = new StreamReader(vertexPath, Encoding.UTF8))
        {
            vertexShaderSource = reader.ReadToEnd();
        }

        string fragmentShaderSource;

        using (StreamReader reader = new StreamReader(fragmentPath, Encoding.UTF8))
        {
            fragmentShaderSource = reader.ReadToEnd();
        }
        
        int vertexShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShader, vertexShaderSource);

        int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShader, fragmentShaderSource);
        
        GL.CompileShader(vertexShader);

        string infoLogVert = GL.GetShaderInfoLog(vertexShader);
        if (infoLogVert != String.Empty)
            Console.WriteLine(infoLogVert);

        GL.CompileShader(fragmentShader);

        string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
        if (infoLogFrag != String.Empty)
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

    private bool _disposedValue = false;

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