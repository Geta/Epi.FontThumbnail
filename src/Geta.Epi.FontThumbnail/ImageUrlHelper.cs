using System;
using System.Configuration;
using System.Linq;
using System.Web;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail
{
    internal static class ImageUrlHelper
    {
        public static string GetUrl(FontAwesome icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
        {
            return BuildSettings(icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        public static string GetUrl(FontAwesome5Brands icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
        {
            return BuildSettings(icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        public static string GetUrl(FontAwesome5Regular icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
        {
            return BuildSettings(icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        public static string GetUrl(FontAwesome5Solid icon, string backgroundColor = "", string foregroundColor = "", int fontSize = -1, Rotations rotate = Rotations.None)
        {
            return BuildSettings(icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        public static string GetUrl(string customFont, int character, string backgroundColor = "", string foregroundColor = "", int fontSize = -1)
        {
            var settings = GetSettings(backgroundColor, foregroundColor, fontSize);

            settings.CustomFontName = customFont;
            settings.Character = character;

            return CompileUrl(settings);
        }

        internal static string BuildSettings(Enum icon, string backgroundColor, string foregroundColor, int fontSize, Rotations rotate)
        {
            var embeddedFont = GetEmbeddedFontLocation(icon);
            return BuildSettings(embeddedFont, (int)(object)icon, backgroundColor, foregroundColor, fontSize, rotate);
        }

        internal static string GetEmbeddedFontLocation(Enum icon)
        {
            switch (icon)
            {
                case FontAwesome _:
                    return "fa4.fonts.fontawesome-webfont.ttf";
                case FontAwesome5Brands _:
                    return "fa5.webfonts.fa-brands-400.ttf";
                case FontAwesome5Regular _:
                    return "fa5.webfonts.fa-regular-400.ttf";
                case FontAwesome5Solid _:
                    return "fa5.webfonts.fa-solid-900.ttf";
                default:
                    return string.Empty;
            }
        }

        internal static string BuildSettings(string embeddedFont, int icon, string backgroundColor, string foregroundColor, int fontSize, Rotations rotate)
        {
            var settings = GetSettings(backgroundColor, foregroundColor, fontSize);
            settings.EmbeddedFont = embeddedFont;
            settings.Character = icon;
            settings.Rotate = rotate;
            return CompileUrl(settings);
        }

        // Helper methods
        private static string CompileUrl(ThumbnailSettings settings)
        {
            var nvc = settings.GetUrlParameters();
            var parameters = string.Join("&", nvc.AllKeys.Select(a => a + "=" + HttpUtility.UrlEncode(nvc[a])));

            return $"/{Constants.UrlFragment}?{parameters}";
        }

        public static ThumbnailSettings GetSettings(string backgroundColor, string foregroundColor, int fontSize)
        {
            var settings = new ThumbnailSettings();

            if (!string.IsNullOrEmpty(backgroundColor))
            {
                settings.BackgroundColor = backgroundColor;
            }
            else
            {
                var cfg = ConfigurationManager.AppSettings[Constants.AppSettings.BackgroundColor] ?? string.Empty;
                settings.BackgroundColor = string.IsNullOrEmpty(cfg) ? Constants.DefaultBackgroundColor : cfg;
            }

            if (!string.IsNullOrEmpty(foregroundColor))
            {
                settings.ForegroundColor = foregroundColor;
            }
            else
            {
                var cfg = ConfigurationManager.AppSettings[Constants.AppSettings.ForegroundColor] ?? string.Empty;
                settings.ForegroundColor = string.IsNullOrEmpty(cfg) ? Constants.DefaultForegroundColor : cfg;
            }

            if (fontSize < 0)
            {
                int.TryParse(ConfigurationManager.AppSettings[Constants.AppSettings.FontSize], out fontSize);
            }

            settings.FontSize = fontSize > 0 ? fontSize : Constants.DefaultFontSize;
            settings.Width = Constants.DefaultWidth;
            settings.Height = Constants.DefaultHeight;

            return settings;
        }
    }
}
