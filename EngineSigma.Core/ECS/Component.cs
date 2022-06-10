namespace EngineSigma.Core.ECS;

public abstract class Component : ICloneable
{
    public Entity Entity = null!;

    internal virtual void Init() {}
    internal virtual void Update() {}
    internal virtual void FixedUpdate() {}

    public abstract object Clone();
}