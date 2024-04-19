using EngiN95.Core.Rendering;
using FluentAssertions;
using NSubstitute;
using OpenTK.Graphics.OpenGL4;

namespace EngineTests.Rendering;

public class VertexArrayTests
{
    private const int Handle = 69;

    [Fact]
    public void TestVertexArrayCtor()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        glWrapper.GenVertexArray().Returns(Handle);
        
        //Act
        var vArray = new VertexArray(glWrapper);
        
        //Assert
        vArray.Handle.Should().Be(Handle);
        glWrapper.Received(1).GenVertexArray();
        glStateHandler.Received(1).UseVertexArray(Handle);
        
        glWrapper.Received(Vertex.ComponentAmount).VertexAttribPointer(
            Arg.Any<int>(), Arg.Any<int>(), Arg.Any<VertexAttribPointerType>(), false, Vertex.Size, Arg.Any<int>());
        glWrapper.Received(Vertex.ComponentAmount).EnableVertexAttribArray(Arg.Any<int>());
    }

    [Fact]
    public void Bind_ShouldBindVertexArray()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        glWrapper.GenVertexArray().Returns(Handle);
        var vArray = new VertexArray(glWrapper);
        
        //Act
        vArray.Bind();
        
        //Assert
        glStateHandler.Received(2).UseVertexArray(Handle);
    }

    [Fact]
    public void UnBind_ShouldUnBindVertexArray()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        glWrapper.GenVertexArray().Returns(Handle);
        var vArray = new VertexArray(glWrapper);    
        
        //Act
        vArray.UnBind();
        
        //Assert
        glStateHandler.Received(1).UseVertexArray(0);
    }

    [Fact]
    public void Dispose_ShouldDeleteVertexArray()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        glWrapper.GenVertexArray().Returns(Handle);
        var vArray = new VertexArray(glWrapper);
        
        //Act
        vArray.Dispose();
        
        //Assert
        glWrapper.Received(1).DeleteVertexArray(Handle);
    }

    [Fact]
    public void Dispose_ShouldReturn_WhenVertexArrayIsAlreadyDisposed()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        glWrapper.GenVertexArray().Returns(Handle);
        var vArray = new VertexArray(glWrapper);
        
        //Act
        vArray.Dispose();
        vArray.Dispose();
        
        //Assert
        glWrapper.Received(1).DeleteVertexArray(Handle);
    }
}