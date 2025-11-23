using System.Drawing;
using NUnit.Framework;

namespace MP3PacEditor.Tests
{
    public class ImageHelpersTests
    {
        [Test]
        public void ConvertIfValid_ReturnsNullWhenInputIsNull()
        {
            string? result = ImageHelpers.ConvertIfValid<string, string>(null, s => s.ToUpperInvariant());
            Assert.That(result, Is.Null);
        }

        [Test]
        public void ConvertIfValid_InvokesConverterWhenInputIsPresent()
        {
            string? result = ImageHelpers.ConvertIfValid("mp3", s => s.ToUpperInvariant());
            Assert.That(result, Is.EqualTo("MP3"));
        }

        [Test]
        public void CropCenterSquare_ReturnsCenteredSquareImage()
        {
            using Bitmap original = new Bitmap(4, 2);
            for (int x = 0; x < original.Width; x++)
            {
                Color color = x < 2 ? Color.Red : Color.Blue;
                for (int y = 0; y < original.Height; y++)
                {
                    original.SetPixel(x, y, color);
                }
            }

            using Image cropped = ImageHelpers.CropCenterSquare(original);

            Assert.That(cropped.Width, Is.EqualTo(cropped.Height));
            Assert.That(cropped.Size, Is.EqualTo(new Size(2, 2)));

            using Bitmap croppedBitmap = new Bitmap(cropped);
            //Assert.That(croppedBitmap.GetPixel(0, 0), Is.EqualTo(Color.Red));
            //Assert.That(croppedBitmap.GetPixel(1, 0), Is.EqualTo(Color.Blue));
            Assert.That(croppedBitmap.GetPixel(0, 0).ToArgb(),Is.EqualTo(Color.Red.ToArgb()));
            Assert.That(croppedBitmap.GetPixel(1, 0).ToArgb(),Is.EqualTo(Color.Blue.ToArgb()));

        }
    }
}