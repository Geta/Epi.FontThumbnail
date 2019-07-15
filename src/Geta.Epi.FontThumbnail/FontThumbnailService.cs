using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.Runtime.InteropServices;
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
            var fileName = settings.GetFileName(".png");
            var cachePath = GetFileFullPath(fileName);

            if (File.Exists(cachePath))
            {
                using (var bmpTemp = new Bitmap(cachePath))
                {
                    return new Bitmap(bmpTemp);
                }
            }
            else
            {
                using (var fileStream = File.Create(cachePath))
                using (var stream = GenerateImage(settings))
                using (var bmpTemp = new Bitmap(stream))
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    stream.CopyTo(fileStream);
                    stream.Dispose();
                    stream.Close();

                    return new Bitmap(bmpTemp);
                }
            }
        }

        internal virtual MemoryStream GenerateImage(ThumbnailSettings settings)
        {
            FontFamily family;
            if (settings.UseEmbeddedFont)
            {
                family = LoadFontFamilyFromEmbeddedResource(settings.EmbeddedFont);
            }
            else
            {
                family = LoadFontFamilyFromDisk(settings.CustomFontName);
            }

            var cc = new ColorConverter();
            var bg = (Color)cc.ConvertFrom(settings.BackgroundColor);
            var fg = (Color)cc.ConvertFrom(settings.ForegroundColor);

            var stream = new MemoryStream();

            using (var font = new Font(family, settings.FontSize))
            using (var bitmap = new Bitmap(settings.Width, settings.Height, PixelFormat.Format24bppRgb))
            using (var g = Graphics.FromImage(bitmap))
            {
                g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(bg);

                switch (settings.Rotate)
                {
                    case Rotations.Rotate90:
                    case Rotations.Rotate180:
                    case Rotations.Rotate270:
                        g.TranslateTransform(settings.Width / 2, settings.Height / 2);
                        g.RotateTransform((int)settings.Rotate);
                        g.TranslateTransform(-(settings.Width / 2), -(settings.Height / 2));
                        break;
                }

                var format1 = new StringFormat(StringFormatFlags.NoClip);
                using (var format = new StringFormat(StringFormatFlags.NoClip))
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    var displayRectangle = new Rectangle(new Point(0, 0), new Size(settings.Width, settings.Height));
                    var chr = char.ConvertFromUtf32(settings.Character);
                    using (var brush = new SolidBrush(fg))
                    {
                        g.DrawString(chr, font, brush, displayRectangle, format);
                    }
                }

                switch (settings.Rotate)
                {
                    case Rotations.FlipHorizontal:
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case Rotations.FlipVertical:
                        bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
                        break;
                }
                bitmap.Save(stream, ImageFormat.Png);
            }

            family.Dispose();
            return stream;
        }

        protected virtual FontFamily LoadFontFamilyFromEmbeddedResource(string fileName)
        {
            var cache = MemoryCache.Default;
            var cacheKey = $"geta.fontawesome.embedded.fontcollection.{fileName}";

            if (!(cache[cacheKey] is PrivateFontCollection fontCollection))
            {
                try
                {
                    fontCollection = new PrivateFontCollection();

                    // specify embedded resource name
                    var resource = $"{Constants.EmbeddedFontPath}.{fileName}";
                    // receive resource stream
                    var fontStream = typeof(FontThumbnailService).Assembly.GetManifestResourceStream(resource);
                    // create an unsafe memory block for the font data
                    var data = Marshal.AllocCoTaskMem((int)fontStream.Length);
                    // create a buffer to read in to
                    var fontdata = new byte[fontStream.Length];
                    // read the font data from the resource
                    fontStream.Read(fontdata, 0, (int)fontStream.Length);
                    // copy the bytes to the unsafe memory block
                    Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);
                    // pass the font to the font collection
                    fontCollection.AddMemoryFont(data, (int)fontStream.Length);
                    // close the resource stream
                    fontStream.Close();
                    fontStream.Dispose();
                    // free the unsafe memory
                    Marshal.FreeCoTaskMem(data);

                    cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to load font {fileName} from EmbeddedResource", ex);
                }
            }

            return fontCollection.Families[0];
        }

        protected virtual FontFamily LoadFontFamilyFromDisk(string fileName)
        {
            var cache = MemoryCache.Default;
            var cacheKey = $"geta.fontawesome.disk.fontcollection.{fileName}";

            if (!(cache[cacheKey] is PrivateFontCollection fontCollection))
            {
                var customFontFolder = ConfigurationManager.AppSettings[Constants.AppSettings.CustomFontPath] ?? Constants.DefaultCustomFontPath;
                var fontPath = $"{customFontFolder}{fileName}";

                var rebased = VirtualPathUtilityEx.RebasePhysicalPath(fontPath);

                try
                {
                    fontCollection = new PrivateFontCollection();
                    fontCollection.AddFontFile(rebased);
                    RemoveFontResourceEx(rebased, 16, IntPtr.Zero);
                    cache.Set(cacheKey, fontCollection, DateTimeOffset.Now.AddMinutes(5));
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unable to load custom font from path {fontPath}", ex);
                }
            }

            return fontCollection.Families[0];
        }

        protected virtual string GetFileFullPath(string fileName)
        {
            string rootPath = ConfigurationManager.AppSettings[Constants.AppSettings.CachePath] ?? Constants.DefaultCachePath;

            return VirtualPathUtilityEx.RebasePhysicalPath(rootPath + fileName);
        }

        // https://stackoverflow.com/questions/26671026/how-to-delete-the-file-of-a-privatefontcollection-addfontfile
        // Force unregister of font in GDI32 because of bug
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int RemoveFontResourceEx(string lpszFilename, int fl, IntPtr pdv);
    }
}
