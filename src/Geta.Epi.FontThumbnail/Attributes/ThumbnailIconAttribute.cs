using System;
using EPiServer.DataAnnotations;

namespace Geta.Epi.FontThumbnail
{
    /// <summary>Assigns an icon to be used when rendering the preview image.</summary>
    /// <remarks>
    /// Used by <see cref="T:EPiServer.DataAbstraction.ContentType" /> to set the image rendered for the preview when creating the content.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ThumbnailIconAttribute : ImageUrlAttribute
    {
        internal Enum Icon { get; set; }
        internal Rotations Rotate { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        public ThumbnailIconAttribute(FontAwesome icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        public ThumbnailIconAttribute(FontAwesome5Brands icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        public ThumbnailIconAttribute(FontAwesome5Regular icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        public ThumbnailIconAttribute(FontAwesome5Solid icon)
            : base(ImageUrlHelper.GetUrl(icon))
        {
            Icon = icon;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ThumbnailIconAttribute(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ThumbnailIconAttribute(FontAwesome5Brands icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ThumbnailIconAttribute(FontAwesome5Regular icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public ThumbnailIconAttribute(FontAwesome5Solid icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ThumbnailIconAttribute(FontAwesome icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ThumbnailIconAttribute(FontAwesome5Brands icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ThumbnailIconAttribute(FontAwesome5Regular icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ThumbnailIconAttribute(FontAwesome5Solid icon, Rotations rotate = Rotations.None, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(icon, backgroundColor, foregroundColor, fontSize, rotate))
        {
            Icon = icon;
            Rotate = rotate;
        }

        
        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.ThumbnailIconAttribute" /> class.
        /// </summary>
        /// <param name="customFont"></param>
        /// <param name="character"></param>
        /// <param name="backgroundColor">The backgroundColor to be used when rendering the image (specified in hexadecimal, for example #000000)</param>
        /// <param name="foregroundColor">The foregroundColor to be used when rendering the image (specified in hexadecimal, for example #ffffff) </param>
        /// <param name="fontSize">The fontSize to be used, default value is 40</param>
        public ThumbnailIconAttribute(string customFont, int character, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
            : base(ImageUrlHelper.GetUrl(customFont, character, backgroundColor, foregroundColor, fontSize))
        {
        }
    }
}
