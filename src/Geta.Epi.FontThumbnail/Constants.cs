namespace Geta.Epi.FontThumbnail
{
    public static class Constants
    {
        public static string ModuleName = "Geta.Epi.FontThumbnail";
        public static string UrlFragment = "Geta_Epi_ThumbnailIcon";
        public static string DefaultCachePath = "[appDataPath]\\thumb_cache\\";
        public static string DefaultCustomFontPath = "[appDataPath]\\fonts\\";
        public static string EmbeddedFontPath = "Geta.Epi.FontThumbnail.ClientResources";
        public static string DefaultBackgroundColor = "#02423F";
        public static string DefaultForegroundColor = "#ffffff";
        public static int DefaultFontSize = 40;
        public static int DefaultWidth = 120;
        public static int DefaultHeight = 90;

        internal static class AppSettings {
            internal const string CustomFontPath = "FontThumbnail.CustomFontPath";
            internal const string CachePath = "FontThumbnail.CachePath";
            internal const string BackgroundColor = "FontThumbnail.BackgroundColor";
            internal const string ForegroundColor = "FontThumbnail.ForegroundColor";
            internal const string FontSize = "FontThumbnail.FontSize";
            internal const string EnableTreeIcons = "FontThumbnail.EnableTreeIcons";
        }
    }
}
