using EngineSigma.GFX.Shaders;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace EngineSigma.GFX;

internal sealed class Renderer
{
    private readonly List<Sprite> _sprites;
    private readonly Matrix4 _projection;

    private readonly Matrix4 _view = Matrix4.Identity + Matrix4.CreateTranslation(0f, 0f, -5f);

    public Renderer()
    {
        _sprites = new List<Sprite>();

        //Set Projection Matrix to Orthographic
        _projection = Matrix4.CreateOrthographic(Window.Width, Window.Height, 1f, 50f);
    }

    public void Render(Shader shader)
    {
        foreach (var sprite in _sprites)
        {
            //Bind VertexArray and IndexBuffer
            GL.BindVertexArray(sprite.VertexArray.VertexArrayHandle);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, sprite.IndexBuffer.IndexBufferHandle);

            //Get Model Matrix
            var transform = sprite.Transform;

            //Multiply with View Matrix for correct Camera Position
            //transform *= _view;

            //Multiply with Projection Matrix for correct Projection
            //transform *= _projection;

            //Pass Matrix to Shader
            shader.SetMatrix4("transform", transform);

            //Draw Vertices
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
        }
    }

    public void AddSprite(Sprite sprite)
    {
        _sprites.Add(sprite);
    }
}