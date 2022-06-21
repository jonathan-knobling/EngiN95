namespace EngineSigma.Core;

public static class Time
{
    /// <summary>
    /// The total amount of time passed since the start of the game
    /// </summary>
    public static TimeSpan TotalGameTime { get; internal set; }
    
    /// <summary>
    /// Amount of Time passed between this and the Last OnUpdate
    /// </summary>
    public static TimeSpan DeltaTime { get; internal set; }
    
    /// <summary>
    /// Amount of Times FixedUpdate gets Executed per Second
    /// </summary>
    public static int TickSpeed { get; set; }
}