namespace EngiN95.Core;

public static class Time
{
    /// <summary>
    /// The total amount of time passed since the start of the game as TimeSpan
    /// </summary>
    public static TimeSpan TotalGameTimeSpan { get; internal set; }

    /// <summary>
    /// The total amount of time passed since the start of the game in seconds
    /// </summary>
    public static float TotalGameTime => (float) TotalGameTimeSpan.TotalSeconds;
    
    /// <summary>
    /// Amount of Time passed between this and the Last OnUpdate as TimeSpan
    /// </summary>
    public static TimeSpan DeltaTimeSpan { get; internal set; }

    /// <summary>
    /// Amount of Time passed between this and the Last OnUpdate in seconds
    /// </summary>
    public static float DeltaTime => (float) DeltaTimeSpan.TotalSeconds;
    
    /// <summary>
    /// Amount of Times FixedUpdate gets Executed per Second
    /// </summary>
    public static int TickSpeed { get; set; }
}