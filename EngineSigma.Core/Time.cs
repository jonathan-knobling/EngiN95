namespace EngineSigma.Core;

public static class Time
{
    /// <summary>
    /// Amount of Time passed between this and the Last Update
    /// </summary>
    public static float DeltaTime { get; internal set; }
    
    /// <summary>
    /// Amount of Times FixedUpdate gets Executed per Second
    /// </summary>
    public static float TickSpeed { get; set; }
}