using OpenTK.Mathematics;

namespace EngineSigma.ECS.Components;

public class Transform : ICloneable
{
    internal Matrix4 TransformMatrix => CalculateTransformMatrix();

    public Vector3 Position { get; set; }
    private Vector3 Scale { get; set; }
    private Quaternion Rotation { get; set; }

    public Transform()
    {
        Position = Vector3.Zero;
        Scale = Vector3.One;
        Rotation = Quaternion.Identity;
    }
    
    private Matrix4 CalculateTransformMatrix()
    {
        var mat = Matrix4.CreateTranslation(Position);
        mat *= Matrix4.CreateScale(Scale);
        mat *= Matrix4.CreateFromQuaternion(Rotation);
        return mat;
    }

    public void ApplyTransformationMatrix(Matrix4 matrix)
    {
        Position += matrix.ExtractTranslation();
        Scale *= matrix.ExtractScale();
        Rotation += matrix.ExtractRotation();
    }

    public void SetTransformationWithMatrix(Matrix4 matrix)
    {
        Position = matrix.ExtractTranslation();
        Scale = matrix.ExtractScale();
        Rotation = matrix.ExtractRotation();
    }

    public object Clone()
    {
        return new Transform()
        {
            Position = Position,
            Scale = Scale,
            Rotation = Rotation
        };
    }
}