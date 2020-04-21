using ImageResizer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using static Services.ImageService;

namespace Services
{
    public static class ImageOptimization
    {
        public static ImageExtension GetImageExtension(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");     //BMP
            var gif = Encoding.ASCII.GetBytes("GIF");    //GIF
            var png = new byte[] { 137, 80, 78, 71 };   // PNG
            var jpeg = new byte[] { 255, 216, 255, 224 }; //JPEG
            var jpeg2 = new byte[] { 255, 216, 255, 225 };

            if(bmp.SequenceEqual(bytes.Take(bmp.Length)))
            {
                return ImageExtension.bmp;
            }

            if(gif.SequenceEqual(bytes.Take(gif.Length)))
            {
                return ImageExtension.gif;
            }

            if(png.SequenceEqual(bytes.Take(png.Length)))
            {
                return ImageExtension.png;
            }

            if(jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
            {
                return ImageExtension.jpeg;
            }

            if(jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
            {
                return ImageExtension.jpeg;
            }

            return ImageExtension.unknown;

        }

        public static bool ValidateImage(byte[] image)
        {
            if(GetImageExtension(image) != ImageExtension.unknown)
            {
                return true;
            }
            return false;
        }

        public static byte[] OptimizeImageFromBytes( int imgWidth, int imgHeight, byte[] imgBytes)
        {
            var settings = new ResizeSettings
            {
                MaxWidth = imgWidth,
                MaxHeight = imgHeight
            };

            MemoryStream ms = new MemoryStream();
            ImageBuilder.Current.Build(imgBytes, ms, settings);
            return ms.ToArray();

        }
 
    }
}