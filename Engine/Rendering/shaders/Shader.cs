using System.Text;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace EngineSigma.Engine.Rendering.shaders;

public sealed class Shader
{
    public int Handle { get; }
    
    private readonly Dictionary<string, int> _uniformLocations;
    
    private bool _disposedValue;

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
        
        //Get Number of Uniforms
        GL.GetProgram(Handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);

        //Create Dictionary to store Uniform locations
        _uniformLocations = new Dictionary<string, int>();

        for (var i = 0; i < numberOfUniforms; i++)
        {
            //Get Name of the Uniform
            var key = GL.GetActiveUniform(Handle, i, out _, out _);

            //Get Location of the Uniform
            var location = GL.GetUniformLocation(Handle, key);
            
            _uniformLocations.Add(key, location);
        }
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }
    
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

    public void SetInt(string name, int data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }
    
    public void SetFloat(string name, float data)
    {
        GL.UseProgram(Handle);
        GL.Uniform1(_uniformLocations[name], data);
    }
    
    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(Handle);
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }
    
    public void SetVector3(string name, Vector3 data)
    {
        GL.UseProgram(Handle);
        GL.Uniform3(_uniformLocations[name], data);
    }
}