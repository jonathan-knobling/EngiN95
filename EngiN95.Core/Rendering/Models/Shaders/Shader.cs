using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace EngiN95.Core.Rendering;

public class Shader : IShader
{
    public int ShaderHandle { get; private set; }
    public bool Compiled { get; private set; }
    
    private readonly IDictionary<string, int> _uniformLocations;
    private ShaderProgramSource ShaderProgramSource { get; }
    
    public Shader(ShaderProgramSource shaderProgramSource, bool compile = true)
    {
        ShaderProgramSource = shaderProgramSource;
        _uniformLocations = new Dictionary<string, int>();
        if (compile) CompileShader();
    }

    public int GetUniformLocation(string uniformName) => _uniformLocations[uniformName];

    public bool CompileShader()
    {
        if (ShaderProgramSource == null) throw new NullReferenceException($"'{nameof(ShaderProgramSource)}' cannot be null");
        if (Compiled)
        {
            Console.WriteLine($"Shader '{this}' is already compiled");
            return false;
        }
        
        int vertexShaderID = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vertexShaderID, ShaderProgramSource.VertexShaderSource);
        GL.CompileShader(vertexShaderID);
        GL.GetShader(vertexShaderID, ShaderParameter.CompileStatus, out int vertexShaderCompCode);
        if (vertexShaderCompCode != (int) All.True)
        {
            Console.WriteLine(GL.GetShaderInfoLog(vertexShaderID));
            return false;
        }

        int fragmentShaderID = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fragmentShaderID, ShaderProgramSource.FragmentShaderSource);
        GL.CompileShader(fragmentShaderID);
        GL.GetShader(fragmentShaderID, ShaderParameter.CompileStatus, out int fragmentShaderCompCode);
        if (fragmentShaderCompCode != (int) All.True)
        {
            Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderID));
            return false;
        }

        ShaderHandle = GL.CreateProgram();
        GL.AttachShader(ShaderHandle, vertexShaderID);
        GL.AttachShader(ShaderHandle, fragmentShaderID);
        GL.LinkProgram(ShaderHandle);
        
        GL.DetachShader(ShaderHandle, vertexShaderID);
        GL.DetachShader(ShaderHandle, fragmentShaderID);
        GL.DeleteShader(vertexShaderID);
        GL.DeleteShader(fragmentShaderID);

        GL.GetProgram(ShaderHandle, GetProgramParameterName.ActiveUniforms, out int uniformCount);
        for (var i = 0; i < uniformCount; i++)
        {
            string key = GL.GetActiveUniform(ShaderHandle, i, out _, out _);
            int location = GL.GetUniformLocation(ShaderHandle, key);
            _uniformLocations.Add(key, location);
        }
        
        Compiled = true;
        return true;
    }

    public void Use()
    {
        if (Compiled)
        {
            GL.UseProgram(ShaderHandle);
            return;
        }
        Console.WriteLine($"Shader '{this}' has not been Compiled yet");
    }
    
    public void SetInt(string name, int data)
    {
        GL.UseProgram(ShaderHandle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetFloat(string name, float data)
    {
        GL.UseProgram(ShaderHandle);
        GL.Uniform1(_uniformLocations[name], data);
    }

    public void SetMatrix4(string name, Matrix4 data)
    {
        GL.UseProgram(ShaderHandle);
        GL.UniformMatrix4(_uniformLocations[name], true, ref data);
    }

    public void SetVector3(string name, Vector3 data)
    {
        GL.UseProgram(ShaderHandle);
        GL.Uniform3(_uniformLocations[name], data);
    }
}