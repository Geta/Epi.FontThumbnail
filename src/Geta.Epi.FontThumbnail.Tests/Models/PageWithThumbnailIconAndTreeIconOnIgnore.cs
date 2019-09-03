using EPiServer.Core;

namespace Geta.Epi.FontThumbnail.Tests.Models
{
    [ThumbnailIcon(FontAwesome5Solid.Anchor)]
    [TreeIcon(Ignore = true)]
    public class PageWithThumbnailIconAndTreeIconOnIgnore : PageData
    {
    }
}
