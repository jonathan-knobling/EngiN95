namespace EngineSigma.Core.ECS;

public abstract class Component : ICloneable
{
    /// <summary>
    /// The Entity this Component is Attatched to
    /// </summary>
    public Entity Entity = null!;

    internal void InternalInit() => Init();
    internal void InternalUpdate() => Update();
    internal void InternalFixedUpdate() => FixedUpdate();

    /// <summary>
    /// Gets called when this Component Gets added to an Entity
    /// </summary>
    protected virtual void Init() {}
    
    /// <summary>
    /// Gets called every time the Window is Updated
    /// </summary>
    protected virtual void Update() {}
    
    /// <summary>
    /// Gets called a fixed amount of Times per second
    /// See Time.TickSpeed
    /// Good For Physics Calculations
    /// </summary>
    protected virtual void FixedUpdate() {}

    public abstract object Clone();
}