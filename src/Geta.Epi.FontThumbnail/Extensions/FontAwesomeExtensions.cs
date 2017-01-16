using System.Configuration;
using System.Linq;
using System.Web;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail.Extensions
{
	public static class FontAwesomeExtensions
	{
		public static string GetUrl(this FontAwesome icon, string backgroundColor, string foregroundColor, int fontSize)
		{
			var settings = new ThumbnailSettings {Icon = icon};

			// backgroundcolor
			if (!string.IsNullOrEmpty(backgroundColor))
			{
				settings.BackgroundColor = backgroundColor;
			}
			else
			{
				var cfg = ConfigurationManager.AppSettings["FontThumbnail.BackgroundColor"] ?? string.Empty;
				settings.BackgroundColor = (string.IsNullOrEmpty(cfg)) ? Constants.DefaultBackgroundColor : cfg;
			}

			// foregroundcolor
			if (!string.IsNullOrEmpty(foregroundColor))
			{
				settings.ForegroundColor = foregroundColor;
			}
			else
			{
				var cfg = ConfigurationManager.AppSettings["FontThumbnail.ForegroundColor"] ?? string.Empty;
				settings.ForegroundColor = (string.IsNullOrEmpty(cfg)) ? Constants.DefaultForegroundColor : cfg;

			}

		    if (fontSize < 0)
		    {
                int.TryParse(ConfigurationManager.AppSettings["FontThumbnail.FontSize"], out fontSize);
                
            }
            settings.FontSize = fontSize > 0 ? fontSize : Constants.DefaultFontSize;

			settings.Width = Constants.DefaultWidth;
			settings.Height = Constants.DefaultHeight;
			
			var nvc = settings.GetUrlParameters();
            string parameters = string.Join("&",nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])));




            return string.Format("thumbnailicon?{0}",parameters);

		}
	}
}