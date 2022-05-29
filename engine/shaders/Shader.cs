using System.Text;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.engine.shaders;

public sealed class Shader
{
    private readonly int _handle;

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
        
        _handle = GL.CreateProgram();

        GL.AttachShader(_handle, vertexShader);
        GL.AttachShader(_handle, fragmentShader);

        GL.LinkProgram(_handle);
        
        GL.DetachShader(_handle, vertexShader);
        GL.DetachShader(_handle, fragmentShader);
        GL.DeleteShader(fragmentShader);
        GL.DeleteShader(vertexShader);
    }

    public void Use()
    {
        GL.UseProgram(_handle);
    }

    private bool _disposedValue = false;

    private void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        
        GL.DeleteProgram(_handle);

        _disposedValue = true;
    }

    ~Shader()
    {
        GL.DeleteProgram(_handle);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}