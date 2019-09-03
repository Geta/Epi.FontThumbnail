using EPiServer.Core;

namespace Geta.Epi.FontThumbnail.Tests.Models
{
    [ThumbnailIcon(FontAwesome5Solid.Wind, Rotations.Rotate180)]
    [TreeIcon(FontAwesome5Regular.Flag, Rotations.Rotate90)]
    public class PageWithThumbnailIconAndDifferentTreeIconRotation : PageData
    {
    }
}
