using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.Mvc;

namespace Geta.Epi.FontThumbnail.Mvc
{
    public class ImageResult : ActionResult
    {
        public Image Image { get; set; }

        public ImageFormat ImageFormat { get; set; }

        private static Dictionary<ImageFormat, string> FormatMap { get; set; }

        static ImageResult()
        {
            CreateContentTypeMap();
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (Image == null) throw new ArgumentNullException(nameof(Image));
            if (ImageFormat == null) throw new ArgumentNullException(nameof(ImageFormat));

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = FormatMap[ImageFormat];

            Image.Save(context.HttpContext.Response.OutputStream, ImageFormat);
        }

        private static void CreateContentTypeMap()
        {
            FormatMap = new Dictionary<ImageFormat, string>
            {
                { ImageFormat.Bmp,  "image/bmp"                },
                { ImageFormat.Gif,  "image/gif"                },
                { ImageFormat.Icon, "image/vnd.microsoft.icon" },
                { ImageFormat.Jpeg, "image/Jpeg"               },
                { ImageFormat.Png,  "image/png"                },
                { ImageFormat.Tiff, "image/tiff"               },
                { ImageFormat.Wmf,  "image/wmf"                }
            };
        }
    }
}
