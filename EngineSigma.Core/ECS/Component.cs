namespace EngineSigma.Core.ECS;

public abstract class Component : ICloneable
{
    /// <summary>
    /// The Entity this Component is Attatched to
    /// </summary>
    public Entity Entity = null!;

    /// <summary>
    /// Gets called when this Component Gets added to an Entity
    /// </summary>
    internal virtual void Init() {}
    
    /// <summary>
    /// Gets called every time the Window is Updated
    /// </summary>
    internal virtual void Update() {}
    
    /// <summary>
    /// Gets called a fixed amount of Times per second
    /// See Time.TickSpeed
    /// Good For Physics Calculations
    /// </summary>
    internal virtual void FixedUpdate() {}

    public abstract object Clone();
}