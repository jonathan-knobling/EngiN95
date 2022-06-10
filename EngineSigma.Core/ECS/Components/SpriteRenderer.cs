using EngineSigma.Core.GFX;
using EngineSigma.Core.GFX.Shaders;
using OpenTK.Graphics.OpenGL;

namespace EngineSigma.Core.ECS.Components;

public sealed class SpriteRenderer : Component
{
    /// <summary>
    /// The Sprite this SpriteRenderer Renders
    /// </summary>
    public Sprite Sprite { get; set; }

    /// <summary>
    /// Renders this Sprite
    /// </summary>
    /// <param name="shader">The Shader Program to Render the Sprite with</param>
    internal void Render(Shader shader)
    {
        //Bind VertexArray and IndexBuffer
        GL.BindVertexArray(Sprite.VertexArray.VertexArrayHandle);
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, Sprite.IndexBuffer.IndexBufferHandle);

        //Get Model Matrix
        var transform = Entity.Transform.TransformMatrix;

        //Multiply with View Matrix for correct Camera Position
        //transform *= _view;

        //Multiply with Projection Matrix for correct Projection
        //transform *= _projection;

        //Pass Matrix to Shader
        shader.SetMatrix4("transform", transform);
        
        //Draw Vertices
        GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    }

    public override object Clone()
    {
        return new SpriteRenderer()
        {
            Sprite = Sprite
        };
    }
}