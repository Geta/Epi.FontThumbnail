using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    public static class ImageUrlHelper
    {
        public static string GetUrl(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
        {
            var settings = GetSettings(backgroundColor,foregroundColor,fontSize);
            settings.EmbeddedFont = "fontawesome.ttf";

            settings.Character = (int)icon;

            return CompileUrl(settings);
        }

        public static string GetUrl(string customFont, int character, string backgroundColor = "",
            string foregroundColor = "", int fontSize = -1)
        {
            var settings = GetSettings(backgroundColor, foregroundColor, fontSize);

            settings.CustomFontName = customFont;
            settings.Character = character;

            return CompileUrl(settings);
        }



        // Helper methods
        private static string CompileUrl(ThumbnailSettings settings)
        {
            var nvc = settings.GetUrlParameters();
            string parameters = string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])));

            return $"/{Constants.UrlFragment}?{parameters}";
        }

        public static ThumbnailSettings GetSettings(string backgroundColor, string foregroundColor, int fontSize)
        {
            var settings = new ThumbnailSettings();

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

            return settings;
        }
    }
}