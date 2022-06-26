using OpenTK.Mathematics;

namespace EngineSigma.Core.Extensions;

public static class Vector2Extensions
{
    /// <summary>
    /// Converts a Vector2 into a Vector3 by setting z to 0
    /// </summary>
    /// <param name="vec2">The Vector2 to Convert</param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 vec2)
    {
        return new Vector3(vec2.X, vec2.Y, 0);
    }

    /// <summary>
    /// Converts a Vector2 into a Vector3
    /// </summary>
    /// <param name="vec2">The Vector2 to Convert</param>
    /// <param name="z">The z component of the new Vector3</param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector2 vec2, float z)
    {
        return new Vector3(vec2.X, vec2.Y, z);
    }
    
    /// <summary>
    /// Converts a Vector2 into a Vector4 by setting z and w to 0
    /// </summary>
    /// <param name="vec2">The Vector2 to Convert</param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector2 vec2)
    {
        return new Vector4(vec2.X, vec2.Y, 0, 0);
    }

    /// <summary>
    /// Converts a Vector2 into a Vector4
    /// </summary>
    /// <param name="vec2">The Vector2 to Convert</param>
    /// <param name="z">The Z Component of the new Vector4</param>
    /// <param name="w">The W Component of the new Vector4</param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector2 vec2, float z, float w)
    {
        return new Vector4(vec2.X, vec2.Y, z, w);
    }
}

public static class Vector3Extensions
{
    /// <summary>
    /// Converts a Vector3 to a Vector2 by ignoring its Z Component
    /// </summary>
    /// <param name="vec3"></param>
    /// <returns></returns>
    public static Vector2 ToVector2(this Vector3 vec3)
    {
        return new Vector2(vec3.X, vec3.Y);
    }
    
    /// <summary>
    /// Converts a Vector4 into a Vector4 by setting w to 0
    /// </summary>
    /// <param name="vec3">The Vector3 to Convert</param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector3 vec3)
    {
        return new Vector4(vec3.X, vec3.Y, vec3.Z, 0);
    }

    /// <summary>
    /// Converts a Vector3 into a Vector4
    /// </summary>
    /// <param name="vec3">The Vector3 to Convert</param>
    /// <param name="w">The W Component of the new Vector4</param>
    /// <returns></returns>
    public static Vector4 ToVector4(this Vector3 vec3, float w)
    {
        return new Vector4(vec3.X, vec3.Y, vec3.Z, w);
    }
}

public static class Vector4Extensions
{
    /// <summary>
    /// Converts a Vector4 to a Vector2 by ignoring its Z and W Components
    /// </summary>
    /// <param name="vec4">The Vector4 to Convert</param>
    /// <returns></returns>
    public static Vector2 ToVector2(this Vector4 vec4)
    {
        return vec4.Xy;
    }

    /// <summary>
    /// Converts a Vector4 to a Vector3 by ignoring its W Component
    /// </summary>
    /// <param name="vec4">The Vector4 to Convert</param>
    /// <returns></returns>
    public static Vector3 ToVector3(this Vector4 vec4)
    {
        return vec4.Xyz;
    }
}