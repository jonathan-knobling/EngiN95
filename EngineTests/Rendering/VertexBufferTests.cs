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
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        
        //Act
        var vBuffer = new VertexBuffer(glWrapper, 128);
        
        //Assert
        vBuffer.Handle.Should().Be(bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).GenBuffer();
        glWrapper.Received(Quantity.Exactly(1)).BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).BufferData(BufferTarget.ArrayBuffer, Arg.Any<int>(), Arg.Any<IntPtr>(), BufferUsageHint.DynamicDraw);
    }

    [Fact]
    public void BufferData_ShouldBufferData()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        var data = new Vertex[12];

        //Act
        vBuffer.BufferData(data);

        //Assert
        glWrapper.Received(Quantity.AtLeastOne()).BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
        glWrapper.Received(Quantity.Exactly(1)).BufferSubData(
            BufferTarget.ArrayBuffer, Arg.Any<IntPtr>(), Arg.Any<int>(), data);
        vBuffer.Elements.Should().Be(data.Length);
    }

    [Fact]
    public void BufferData_ShouldReturnIfDataIsEmpty()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        var data = Array.Empty<Vertex>();
        
        //Act
        vBuffer.BufferData(data);
        
        //Assert
        glWrapper.Received(Quantity.Exactly(1)).BindBuffer(Arg.Any<BufferTarget>(), bufferHandle);
        glWrapper.Received(Quantity.None()).BufferSubData(BufferTarget.ArrayBuffer, Arg.Any<IntPtr>(), Arg.Any<int>(), data);
    }

    [Fact]
    public void Bind_ShouldBindTheBuffer()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        
        //Act
        vBuffer.Bind();
        
        //Assert
        glWrapper.Received(Quantity.AtLeastOne()).BindBuffer(BufferTarget.ArrayBuffer, bufferHandle);
    }

    [Fact]
    public void UnBind_ShouldUnBindTheBuffer()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        
        //Act
        vBuffer.UnBind();
        
        //Assert
        glWrapper.Received(Quantity.Exactly(1)).BindBuffer(BufferTarget.ArrayBuffer, 0);
    }

    [Fact]
    public void Dispose_ShouldDeleteBufferObject()
    {
        //Arrange
        var glWrapper = Substitute.For<IGLWrapper>();
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        
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
        const int bufferHandle = 69;
        glWrapper.GenBuffer().Returns(bufferHandle);
        var vBuffer = new VertexBuffer(glWrapper);
        
        //Act
        vBuffer.Dispose();
        vBuffer.Dispose();
        
        //Assert
        glWrapper.Received(Quantity.Exactly(1)).DeleteBuffer(bufferHandle);
    }
}