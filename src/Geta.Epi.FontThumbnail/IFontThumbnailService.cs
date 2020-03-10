using System.Drawing;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    public interface IFontThumbnailService
    {
        /// <summary>
        /// Loads or creates a thumbnail using the given settings
        /// </summary>
        /// <param name="settings">The ThumbnailSettings parameter</param>
        /// <returns></returns>
        Image LoadThumbnailImage(ThumbnailSettings settings);
    }
}