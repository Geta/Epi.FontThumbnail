using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using System.Web.Mvc;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.Configuration;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Logging.Compatibility;
using EPiServer.Web.Mvc;
using Geta.Epi.FontThumbnail.Mvc;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail.Controllers
{
	public class ThumbnailIconController : Controller
	{
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IFontThumbnailService _thumbnailService;
        
        public ThumbnailIconController(IFontThumbnailService thumbnailService)
	    {
	        if (thumbnailService == null) throw new ArgumentNullException(nameof(thumbnailService));
	        _thumbnailService = thumbnailService;
	    }

        [Authorize(Roles = "Administrators, CmsAdmins, CmsEditors, WebAdmins, WebEditors")]
        public ActionResult GenerateThumbnail(ThumbnailSettings settings)
	    {
            if (!CheckValidFormatHtmlColor(settings.BackgroundColor) || !CheckValidFormatHtmlColor(settings.ForegroundColor))
            {
                throw (new Exception("Unknown foreground or background color"));
            }
            
	        var image = _thumbnailService.LoadThumbnailImage(settings);

	        return new ImageResult() {Image = image, ImageFormat = ImageFormat.Png};
	    }

        protected static bool CheckValidFormatHtmlColor(string inputColor)
        {
            if (Regex.Match(inputColor, "^#(?:[0-9a-fA-F]{3}){1,2}$").Success)
                return true;

            var result = System.Drawing.Color.FromName(inputColor);
            return result.IsKnownColor;
        }

	   
	}
}