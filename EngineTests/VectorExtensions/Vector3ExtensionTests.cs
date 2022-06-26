using EngineSigma.Core.Extensions;
using FluentAssertions;
using OpenTK.Mathematics;

namespace EngineTests;

public class Vector3Extensions
{
    [Fact]
    public void ConvertToVec2()
    {
        var vec3 = new Vector3(2,3,4);

        var result = vec3.ToVector2();

        result.Should().Be(vec3.Xy);
    }

    [Fact]
    public void ConvertToVec4_WithDefaultW()
    {
        var vec3 = new Vector3(2, 3, 4);

        var result = vec3.ToVector4();

        result.Xyz.Should().Be(vec3);
        result.W.Should().Be(0);
    }

    [Fact]
    public void ConvertToVec4_WithSpecificW()
    {
        var vec3 = new Vector3(2, 3, 4);

        var result = vec3.ToVector4(23);

        result.Xyz.Should().Be(vec3);
        result.W.Should().Be(23);
    }
}