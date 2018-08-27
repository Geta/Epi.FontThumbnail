using EPiServer.DataAnnotations;
using System;

namespace Geta.Epi.FontThumbnail.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ThumbnailIcon : ImageUrlAttribute
    {
        public ThumbnailIcon(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize))
        {
        }

        public ThumbnailIcon(FontAwesome5Brands icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize))
        {
        }

        public ThumbnailIcon(FontAwesome5Regular icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize))
        {
        }

        public ThumbnailIcon(FontAwesome5Solid icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize))
        {
        }

        public ThumbnailIcon(string customFont, int character, string backgroundColor = "", string foregroundColor = "", int fontSize = -1) : base(ImageUrlHelper.GetUrl(customFont, character, backgroundColor, foregroundColor, fontSize))
        {
        }
    }
}