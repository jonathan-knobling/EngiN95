using OpenTK.Mathematics;

namespace EngineSigma.Core.ECS.Components;

// ReSharper disable ConvertToAutoProperty

public sealed class Transform : ICloneable
{
    internal Matrix4 TransformMatrix => CalculateTransformMatrix();

    private Vector3 _position;
    private Vector3 _scale;
    private Quaternion _rotation;
    
    /// <summary>
    /// The Position of this Transform
    /// </summary>
    public Vector3 Position 
    {
        get => _position;
        set => _position = value;
    }

    /// <summary>
    /// The Scale of this Transform
    /// </summary>
    public Vector3 Scale 
    {
        get => _scale;
        set => _scale = value;
    }

    /// <summary>
    /// The Rotation of this Transform
    /// </summary>
    public Quaternion Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }
    
    public Transform()
    {
        _position = Vector3.Zero;
        _scale = Vector3.One;
        _rotation = Quaternion.Identity;
    }

    public Transform(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        _position = position;
        _scale = scale;
        _rotation = rotation;
    }

    public Transform(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _scale = Vector3.One;
        _rotation = rotation;
    }

    public Transform(Vector3 position)
    {
        _position = position;
        _scale = Vector3.One;
        _rotation = Quaternion.Identity;
    }

    public Transform(Quaternion rotation)
    {
        _position = Vector3.Zero;
        _scale = Vector3.One;
        _rotation = rotation;
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