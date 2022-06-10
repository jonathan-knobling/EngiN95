using OpenTK.Mathematics;

namespace EngineSigma.Core.ECS.Components;

public sealed class Transform : ICloneable
{
    internal Matrix4 TransformMatrix => CalculateTransformMatrix();

    /// <summary>
    /// The Position of this Transform
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// The Scale of this Transform
    /// </summary>
    public Vector3 Scale { get; set; }

    /// <summary>
    /// The Rotation of this Transform
    /// </summary>
    public Quaternion Rotation { get; set; }
    
    public Transform()
    {
        Position = Vector3.Zero;
        Scale = Vector3.One;
        Rotation = Quaternion.Identity;
    }

    public Transform(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        Position = position;
        Scale = scale;
        Rotation = rotation;
    }

    public Transform(Vector3 position, Quaternion rotation)
    {
        Position = position;
        Scale = Vector3.One;
        Rotation = rotation;
    }

    public Transform(Vector3 position)
    {
        Position = position;
        Scale = Vector3.One;
        Rotation = Quaternion.Identity;
    }

    public Transform(Quaternion rotation)
    {
        Position = Vector3.Zero;
        Scale = Vector3.One;
        Rotation = rotation;
    }

    /// <summary>
    /// Calculates a Matrix out of this Transforms Position, Scale and Rotation
    /// </summary>
    /// <returns>A Matrix4 calculated out of this Transforms Position, Scale and Rotation</returns>
    private Matrix4 CalculateTransformMatrix()
    {
        var mat = Matrix4.CreateTranslation(Position);
        mat *= Matrix4.CreateScale(Scale);
        mat *= Matrix4.CreateFromQuaternion(Rotation);
        return mat;
    }

    /// <summary>
    /// Multiplies this Transforms Matrix whit the given Matrix
    /// </summary>
    /// <param name="matrix">The Matrix to Multiply onto this Transform</param>
    public void ApplyTransformationMatrix(Matrix4 matrix)
    {
        Position += matrix.ExtractTranslation();
        Scale *= matrix.ExtractScale();
        Rotation += matrix.ExtractRotation();
    }

    /// <summary>
    /// Sets this Transforms Matrix to the given Matrix
    /// </summary>
    /// <param name="matrix">The Matrix to Set this Transform to</param>
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