using System;

namespace Geta.Epi.FontThumbnail
{
    /// <summary>Assigns an icon to be used when the current content is displayed in the Episerver tree view.</summary>
    /// <remarks>
    /// Used by <see cref="T:EPiServer.DataAbstraction.ContentType" /> to set the icon to be displayed in the content tree.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TreeIconAttribute : Attribute
    {
        internal Enum Icon { get; set; }
        internal Rotations Rotate { get; set; }

        /// <summary>
        /// Overwrites the global setting
        /// </summary>
        public bool Ignore { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.TreeIconAttribute" /> class.
        /// </summary>
        public TreeIconAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.TreeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public TreeIconAttribute(FontAwesome icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.TreeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Brands icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public TreeIconAttribute(FontAwesome5Brands icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.TreeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Regular icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public TreeIconAttribute(FontAwesome5Regular icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Geta.Epi.FontThumbnail.TreeIconAttribute" /> class.
        /// </summary>
        /// <param name="icon">The FontAwesome5Solid icon to be used</param>
        /// <param name="rotate">The rotation to be used, defaults to None</param>
        public TreeIconAttribute(FontAwesome5Solid icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }
    }
}
