using System;
using System.Drawing;
using System.IO;

public static class ImageHelpers
{
    public static TResult? ConvertIfValid<TInput, TResult>(TInput input, Func<TInput, TResult> converter)
        where TInput : class
        where TResult : class
    {
        return input != null ? converter(input) : null;
    }

    public static Image CropCenterSquare(Image original)
    {
        int size = Math.Min(original.Width, original.Height);
        int x = (original.Width - size) / 2;
        int y = (original.Height - size) / 2;

        Rectangle cropArea = new Rectangle(x, y, size, size);
        Bitmap cropped = new Bitmap(size, size);

        using (Graphics g = Graphics.FromImage(cropped))
        {
            g.DrawImage(original, new Rectangle(0, 0, size, size), cropArea, GraphicsUnit.Pixel);
        }

        return cropped;
    }
}
