using System;

namespace Geta.Epi.FontThumbnail
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TreeIconAttribute : Attribute
    {
        internal Enum Icon { get; set; }
        internal Rotations Rotate { get; set; }

        /// <summary>
        /// Overwrites the global setting
        /// </summary>
        public bool Ignore { get; set; }

        public TreeIconAttribute()
        {
        }

        public TreeIconAttribute(FontAwesome icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        public TreeIconAttribute(FontAwesome5Brands icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        public TreeIconAttribute(FontAwesome5Regular icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }

        public TreeIconAttribute(FontAwesome5Solid icon, Rotations rotate = Rotations.None)
        {
            Icon = icon;
            Rotate = rotate;
        }
    }
}
