using System;
using EPiServer.DataAnnotations;

namespace Geta.Epi.FontThumbnail
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ThumbnailIconAttribute : ImageUrlAttribute
    {
        internal Enum Icon { get; set; }
        internal Rotations Rotate { get; set; }

        public ThumbnailIconAttribute(FontAwesome icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        public ThumbnailIconAttribute(FontAwesome5Brands icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        public ThumbnailIconAttribute(FontAwesome5Regular icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        public ThumbnailIconAttribute(FontAwesome5Solid icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        public ThumbnailIconAttribute(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Brands icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Regular icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Solid icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Brands icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Regular icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(FontAwesome5Solid icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        public ThumbnailIconAttribute(string customFont, int character, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(customFont, character, backgroundColor, foregroundColor, fontSize))
        {
        }
    }
}
