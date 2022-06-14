using OpenTK.Graphics.OpenGL4;

namespace EngineSigma.Core.Rendering;

public class Shader
{
    public int ShaderHandle { get; private set; }
    public bool Compiled { get; private set; }

    private ShaderProgramSource ShaderProgramSource { get; }
    
    public Shader(ShaderProgramSource shaderProgramSource, bool compile = true)
    {
        ShaderProgramSource = shaderProgramSource;
        if (compile) CompileShader();
    }

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

        Compiled = true;
        return true;
    }

    public void Use()
    {
        if (Compiled) GL.UseProgram(ShaderHandle);
        else Console.WriteLine($"Shader '{this}' has not been Compiled yet");
    }
}