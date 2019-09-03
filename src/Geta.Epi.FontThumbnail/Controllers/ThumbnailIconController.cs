using System;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Geta.Epi.FontThumbnail.Mvc;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail.Controllers
{
    public class ThumbnailIconController : Controller
    {
        private readonly IFontThumbnailService _thumbnailService;

        public ThumbnailIconController(IFontThumbnailService thumbnailService)
        {
            _thumbnailService = thumbnailService ?? throw new ArgumentNullException(nameof(thumbnailService));
        }

        [Authorize(Roles = "Administrators, CmsAdmins, CmsEditors, WebAdmins, WebEditors")]
        public ActionResult GenerateThumbnail(ThumbnailSettings settings)
        {
            if (!CheckValidFormatHtmlColor(settings.BackgroundColor) || !CheckValidFormatHtmlColor(settings.ForegroundColor))
            {
                throw new Exception("Unknown foreground or background color");
            }

            var image = _thumbnailService.LoadThumbnailImage(settings);

            return new ImageResult() { Image = image, ImageFormat = ImageFormat.Png };
        }

        internal bool CheckValidFormatHtmlColor(string inputColor)
        {
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                return true;

            var result = System.Drawing.Color.FromName(inputColor);
            return result.IsKnownColor;
        }
    }
}
