using OpenTK.Mathematics;

namespace EngineSigma.Core.ECS.Components;

public sealed class Rigidbody : Component
{
    private Vector2 _velocity;

    /// <summary>
    /// The Velocity of the Entity this Rigidbody Component is Attached to
    /// </summary>
    public Vector2 velocity
    {
        get => _velocity;
        set => _velocity = value;
    }

    protected override void Update()
    {
        Entity.Transform.Position += new Vector3(_velocity.X, _velocity.Y, 0) * Time.DeltaTime;
    }

    public override object Clone()
    {
        return new Rigidbody()
        {
            _velocity = _velocity
        };
    }
}