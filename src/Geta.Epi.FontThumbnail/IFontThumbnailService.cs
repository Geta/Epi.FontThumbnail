using System.Drawing;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    public interface IFontThumbnailService
    {
        Image LoadThumbnailImage(ThumbnailSettings settings);
    }
}