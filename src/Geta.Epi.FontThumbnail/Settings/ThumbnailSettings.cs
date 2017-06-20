using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Text;

namespace Geta.Epi.FontThumbnail.Settings
{
    public class ThumbnailSettings
    {
        public string BackgroundColor { get; set; }
        public string ForegroundColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int FontSize { get; set; }
        public int Character { get; set; }
        public string CustomFontName { get; set; }
        public string EmbeddedFont { get; set; }

        public bool UseEmbeddedFont
        {
            get
            {
                return string.IsNullOrWhiteSpace(CustomFontName);
            }
        }

        public string GetFileName(string fileEnding)
        {
            return string.Concat(this.AsGuid(this.ToString()),fileEnding);
        }

        public NameValueCollection GetUrlParameters()
        {
            var coll = new NameValueCollection();

            foreach (var prop in this.GetType().GetProperties())
            {
                coll.Add(prop.Name,prop.GetValue(this,null)?.ToString());
            }
            return coll;
        }

        protected virtual Guid AsGuid(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);

            Guid result;

            using (var hasher = MD5.Create())
            {
                var hash = hasher.ComputeHash(bytes);
                result = new Guid(hash);
            }

            return result;
        }

        public override string ToString()
        {
            return string.Concat(this.EmbeddedFont, this.CustomFontName, this.Character, this.BackgroundColor, this.ForegroundColor, this.FontSize, this.Width,
                this.Height);
        }
    }
}