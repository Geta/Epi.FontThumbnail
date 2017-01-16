using System.Drawing;
using EPiServer.HtmlParsing;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    public interface IFontThumbnailService
    {
        Image LoadThumbnailImage(ThumbnailSettings settings);
    }
}