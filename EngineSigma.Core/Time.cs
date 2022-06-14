namespace EngineSigma.Core;

public static class Time
{
    public static TimeSpan TotalGameTime { get; internal set; }
    
    /// <summary>
    /// Amount of Time passed between this and the Last OnUpdate
    /// </summary>
    public static TimeSpan DeltaTime { get; internal set; }
    
    /// <summary>
    /// Amount of Times FixedUpdate gets Executed per Second
    /// </summary>
    public static TimeSpan TickSpeed { get; set; }
}