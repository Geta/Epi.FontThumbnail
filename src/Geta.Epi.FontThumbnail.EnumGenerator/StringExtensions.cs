using System.Globalization;
using System.Linq;

namespace Geta.Epi.FontThumbnail.EnumGenerator
{
    internal static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value);
        }

        public static string FormatSemver(this string value)
        {
            while (value.Count(x => x == '.') < 2)
            {
                value += ".0";
            }

            return value;
        }
    }
}
