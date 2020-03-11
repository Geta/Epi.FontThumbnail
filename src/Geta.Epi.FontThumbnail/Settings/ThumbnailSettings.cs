using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace Geta.Epi.FontThumbnail.Settings
{
    public class ThumbnailSettings
    {
        /// <summary>
        ///  The BackgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)
        /// </summary>
        public string BackgroundColor { get; set; }
        
        /// <summary>
        ///  The ForegroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)
        /// </summary>
        public string ForegroundColor { get; set; }
        
        /// <summary>
        ///  The Width to be used when rendering the image (specified in pixels, for example 120)
        /// </summary>
        public int Width { get; set; }
        
        /// <summary>
        ///  The Height to be used when rendering the image (specified in pixels, for example 90)
        /// </summary>
        public int Height { get; set; }
        
        /// <summary>
        ///  The FontSize to be used when rendering the image (for example 40)
        /// </summary>
        public int FontSize { get; set; }
        
        /// <summary>
        ///  The Character to be used when rendering the image
        /// </summary>
        public int Character { get; set; }
        
        /// <summary>
        ///  The CustomFontName to be used when rendering the image
        /// </summary>
        public string CustomFontName { get; set; }
        
        /// <summary>
        ///  The EmbeddedFont to be used when rendering the image
        /// </summary>
        public string EmbeddedFont { get; set; }
        
        /// <summary>
        ///  Flags if UseEmbeddedFont should be true or fals
        /// </summary>
        public bool UseEmbeddedFont => string.IsNullOrWhiteSpace(CustomFontName);
        
        /// <summary>
        ///  The Rotation to be used when rendering the image
        /// </summary>
        public Rotations Rotate { get; set; }

        /// <summary>
        /// Generates the fileName to be used
        /// </summary>
        /// <param name="fileEnding">the fileEnding to be used, for example .png</param>
        /// <returns></returns>
        public string GetFileName(string fileEnding)
        {
            return string.Concat(AsGuid(ToString()), fileEnding);
        }

        /// <summary>
        /// Generates a collection of url parameters to be passed to the controller
        /// </summary>
        /// <returns></returns>
        public NameValueCollection GetUrlParameters()
        {
            var coll = new NameValueCollection();

            foreach (var prop in GetType().GetProperties())
            {
                coll.Add(prop.Name, prop.GetValue(this, null)?.ToString());
            }
            return coll;
        }

        /// <summary>
        /// Generates a Guid based on the input string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns></returns>
        protected virtual Guid AsGuid(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            using (var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);
                return new Guid(hash);
            }
        }

        /// <summary>
        /// Generates a string based on all properties on the object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Concat(EmbeddedFont ?? string.Empty, CustomFontName, Character, BackgroundColor, ForegroundColor, FontSize, Width, Height, Rotate);
        }
    }
}