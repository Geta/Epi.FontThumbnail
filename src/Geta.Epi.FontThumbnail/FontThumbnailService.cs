using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Web;
using System.Web.Hosting;
using EPiServer.Framework.Configuration;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    [ServiceConfiguration(typeof(IFontThumbnailService))]
    public class FontThumbnailService : IFontThumbnailService
    {
        public virtual Image LoadThumbnailImage(ThumbnailSettings settings)
        {
            string fileName = settings.GetFileName(".png");

            if (!CachedImageExists(fileName))
            {   
                var stream = GenerateImage(settings);

                string savePath = GetFileFullPath(fileName);
                using (var fileStream = File.Create(savePath))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                }
            }
            return Image.FromFile(GetFileFullPath(fileName));
        }

        protected virtual MemoryStream GenerateImage(ThumbnailSettings settings)
        {
            PrivateFontCollection fonts;
            FontFamily family = LoadFontFamily(HttpContext.Current.Server.MapPath("~/fontawesome-webfont.ttf"), out fonts);

            var cc = new ColorConverter();
            var bg = (Color)cc.ConvertFrom(settings.BackgroundColor);
            var fg = (Color)cc.ConvertFrom(settings.ForegroundColor);

            var stream = new MemoryStream();

            using (var font = new Font(family, settings.FontSize))
            using (var bitmap = new Bitmap(settings.Width, settings.Height, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.Clear(bg);

                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);

                format1.LineAlignment = StringAlignment.Center;
                format1.Alignment = StringAlignment.Center;

                Rectangle displayRectangle = new Rectangle(new Point(0, 0), new Size(settings.Width, settings.Height));
                string chr = char.ConvertFromUtf32((int)settings.Icon);
                g.DrawString(chr, font, new SolidBrush(fg), displayRectangle, format1);
                bitmap.Save(stream, ImageFormat.Png);

                font.Dispose();
                family.Dispose();
            }

            return stream;
        }

        protected virtual FontFamily LoadFontFamily(string fileName, out PrivateFontCollection fontCollection)
        {
            fontCollection = new PrivateFontCollection();
            fontCollection.AddFontFile(fileName);
            return fontCollection.Families[0];
        }

        protected virtual bool CachedImageExists(string fileName)
        {
            return File.Exists(GetFileFullPath(fileName));
        }

        protected virtual string GetFileFullPath(string fileName)
        {
            string rootPath = ConfigurationManager.AppSettings["FontThumbnail.CachePath"] ?? Constants.DefaultCachePath;
            
            return VirtualPathUtilityEx.RebasePhysicalPath(rootPath + fileName);
        }

        
    }
}