using EngineSigma.Core.Rendering;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using OpenTK.Graphics.OpenGL4;

namespace EngineTests.Rendering;

public class DynamicVertexBufferTests
{
    [Fact]
    public void CreateVertexBufferTest()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        
        //Act
        var vBuffer = new VertexBuffer(glWrapper, glStateHandler, 128);
        
        //Assert
        vBuffer.Handle.Should().Be(bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).GenBuffer();
        glStateHandler.Received(Quantity.Exactly(1)).UseVertexBuffer(bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).BufferData(BufferTarget.ArrayBuffer, Arg.Any<int>(), Arg.Any<IntPtr>(), BufferUsageHint.DynamicDraw);
    }

    [Fact]
    public void BufferData_ShouldBufferData()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);
        var data = new Vertex[12];

        //Act
        vBuffer.BufferData(data);

        //Assert
        glStateHandler.Received(Quantity.AtLeastOne()).UseVertexBuffer(bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).BufferSubData(
            BufferTarget.ArrayBuffer, Arg.Any<IntPtr>(), Arg.Any<int>(), data);
        vBuffer.Elements.Should().Be(data.Length);
    }

    [Fact]
    public void BufferData_ShouldReturnIfDataIsEmpty()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);
        Vertex[] data = Array.Empty<Vertex>();
        
        //Act
        vBuffer.BufferData(data);
        
        //Assert
        glStateHandler.Received(Quantity.Exactly(1)).UseVertexBuffer(bufferHandle);
        glWrapper.Received(Quantity.None()).BufferSubData(BufferTarget.ArrayBuffer, Arg.Any<IntPtr>(), Arg.Any<int>(), data);
    }

    [Fact]
    public void Bind_ShouldBindTheBuffer()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);

        //Act
        vBuffer.Bind();
        
        //Assert
        glStateHandler.Received(Quantity.AtLeastOne()).UseVertexBuffer(bufferHandle);
    }

    [Fact]
    public void UnBind_ShouldUnBindTheBuffer()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);
        
        //Act
        vBuffer.UnBind();
        
        //Assert
        glStateHandler.Received(Quantity.Exactly(1)).UseVertexBuffer(0);
    }

    [Fact]
    public void Dispose_ShouldDeleteBufferObject()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);
        
        //Act
        vBuffer.Dispose();
        
        //Assert
        glWrapper.Received(Quantity.Exactly(1)).DeleteBuffer(bufferHandle);
    }
    
    [Fact]
    public void Dispose_ShouldReturnIfBufferWasAlreadyDisposed()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        var glStateHandler = Substitute.For<IGLStateHandler>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper,glStateHandler);
        
        //Act
        vBuffer.Dispose();
        vBuffer.Dispose();
        
        //Assert
        glWrapper.Received(Quantity.Exactly(1)).DeleteBuffer(bufferHandle);
    }
}