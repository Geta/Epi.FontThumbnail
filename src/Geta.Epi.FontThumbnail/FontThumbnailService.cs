using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Modules;
using EPiServer.Web;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    [ServiceConfiguration(typeof(IFontThumbnailService))]
    public class FontThumbnailService : IFontThumbnailService
    {
	    private readonly ModuleTable _moduleTable;

	    public FontThumbnailService(ModuleTable moduleTable)
	    {
		    _moduleTable = moduleTable;
	    }

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
                    stream.Dispose();
                    stream.Close();
                }
            }

            Image img;
            using (var bmpTemp = new Bitmap(GetFileFullPath(fileName)))
            {
                img = new Bitmap(bmpTemp);
            }

            return img;
        }

        protected virtual MemoryStream GenerateImage(ThumbnailSettings settings)
        {
			PrivateFontCollection fonts;
            FontFamily family;
            if (settings.UseEmbeddedFont)
            {
                family = LoadFontFamilyFromEmbeddedResource(settings.EmbeddedFont, out fonts);
            }
            else
            {
                family = LoadFontFamilyFromDisk(settings.CustomFontName, out fonts);
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
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.Clear(bg);

                StringFormat format1 = new StringFormat(StringFormatFlags.NoClip);

                format1.LineAlignment = StringAlignment.Center;
                format1.Alignment = StringAlignment.Center;

                Rectangle displayRectangle = new Rectangle(new Point(0, 0), new Size(settings.Width, settings.Height));
                string chr = char.ConvertFromUtf32(settings.Character);
                g.DrawString(chr, font, new SolidBrush(fg), displayRectangle, format1);
                bitmap.Save(stream, ImageFormat.Png);

				family.Dispose();
				fonts.Dispose();
			}

			return stream;
        }

        protected virtual FontFamily LoadFontFamilyFromEmbeddedResource(string fileName, out PrivateFontCollection fontCollection)
        {
            try
            {
                fontCollection = new PrivateFontCollection();

                string fontPath = $"{Constants.EmbeddedFontPath}.{fileName}";

                // receive resource stream
                Stream fontStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fontPath);

                // create an unsafe memory block for the font data
                System.IntPtr data = Marshal.AllocCoTaskMem((int)fontStream.Length);

                // create a buffer to read in to
                byte[] fontdata = new byte[fontStream.Length];

                // read the font data from the resource
                fontStream.Read(fontdata, 0, (int)fontStream.Length);

                // copy the bytes to the unsafe memory block
                Marshal.Copy(fontdata, 0, data, (int)fontStream.Length);

                // pass the font to the font collection
                fontCollection.AddMemoryFont(data, (int)fontStream.Length);

                // close the resource stream
                fontStream.Close();

                // free up the unsafe memory
                Marshal.FreeCoTaskMem(data);

                return fontCollection.Families[0];
            }
            catch(Exception ex)
            {
                throw (new Exception($"Unable to load font {fileName} from EmbeddedResource", ex));
            }
        }

        protected virtual FontFamily LoadFontFamilyFromDisk(string fileName, out PrivateFontCollection fontCollection)
        {
            string customFontFolder = ConfigurationManager.AppSettings["FontThumbnail.CustomFontPath"] ?? Constants.DefaultCustomFontPath;
            string fontPath = $"{customFontFolder}{fileName}";

            var rebased = VirtualPathUtilityEx.RebasePhysicalPath(fontPath);

            try
            {
                fontCollection = new PrivateFontCollection();
                fontCollection.AddFontFile(rebased);
                return fontCollection.Families[0];
            }
            catch (Exception ex)
            {
                throw (new Exception($"Unable to load custom font from path {fontPath}", ex));
            }
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