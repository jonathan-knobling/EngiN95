using OpenTK.Mathematics;

namespace EngineSigma.Core.Rendering;

public interface IShader
{
    int ShaderHandle { get; }
    bool Compiled { get; }
    int GetUniformLocation(string uniformName);
    bool CompileShader();
    void Use();
    void SetInt(string name, int data);
    void SetFloat(string name, float data);
    void SetMatrix4(string name, Matrix4 data);
    void SetVector3(string name, Vector3 data);
}