using ImageMagick;

namespace Micros.Image.Generator.Service.Providers
{
    public class ImageToolsProvider : IImageToolsProvider
    {
        public Image GenerateImage(string title, byte[] imageBytes)
        {
            var background = CreateBackground();
            AddCapture(background, title);
            AddImage(background, imageBytes);

            var image = new Image
            {
                Data = background.ToByteArray(MagickFormat.Jpeg)
            };
            return image;
        }

        private MagickImage CreateBackground()
        {
            var settings = new MagickReadSettings
            {
                Width = 1024,
                Height = 768
            };

            var image = new MagickImage("radial-gradient:gray95-grey60", settings);
            image.ColorType = ColorType.TrueColor;
            return image;
        }

        private void AddCapture(MagickImage baseImage, string captureText)
        {
            var readSettings = new MagickReadSettings
            {
                Font = "Roboto",
                FillColor = new MagickColor("gray20"),
                TextGravity = Gravity.Center,
                BackgroundColor = MagickColors.Transparent,
                Height = 200, // height of text box
                Width = 750, // width of text box
                FontPointsize = 30,
            };

            using (var caption = new MagickImage($"caption:{captureText}", readSettings))
            {
                baseImage.Composite(caption, Gravity.South, CompositeOperator.Over);
            }
        }

        private void AddImage(MagickImage baseImage, byte[] imageBytes)
        {
            using var image = new MagickImage(imageBytes, MagickFormat.Jpeg);
            image.Resize(700, 500);
            image.BorderColor = new MagickColor("gray20");
            image.Border(3);
            baseImage.Composite(image, Gravity.North, new PointD(0, 60), CompositeOperator.Over);
        }
    }

    public class Image
    {
        public byte[] Data { get; set; }
    }
}