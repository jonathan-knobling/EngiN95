using EngineSigma.Core.Extensions;
using OpenTK.Mathematics;

namespace EngineSigma.Core.Rendering;

public sealed class Camera
{
    public static Camera Instance { get; } = new();

    private Vector2 Position { get; set; }

    /// <summary>
    /// Moves the camera along the displacement vector
    /// </summary>
    /// <param name="displacement">Displacement vector in pixels</param>
    public void Move(Vector2 displacement)
    {
        Position += displacement;
    }

    public Matrix4 ToViewMatrix()
    {
        return Matrix4.CreateTranslation(Position.ToVector3());
    }
    
    //not mark type as beforefieldinit
    static Camera() {}
    //no other instance can be created
    private Camera() {}
}