using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Engine.GFX;

public class Image
{
    public Image(string path)
    {
        //Load Image
        var image = SixLabors.ImageSharp.Image.Load<Rgba32>(path);

        //Flip Image For Use in OpenGL
        image.Mutate(x => x.Flip(FlipMode.Vertical));

        //Convert to byte array for use in OpenGL
        var pixels = new List<byte>(4 * image.Width * image.Height);

        for (var y = 0; y < image.Height; y++)
        for (var x = 0; x < image.Width; x++)
        {
            pixels.Add(image[x, y].R);
            pixels.Add(image[x, y].G);
            pixels.Add(image[x, y].B);
            pixels.Add(image[x, y].A);
        }

        PixelData = pixels.ToArray();
        Width = image.Width;
        Height = image.Height;
    }

    public byte[] PixelData { get; }
    public int Width { get; }
    public int Height { get; }
}