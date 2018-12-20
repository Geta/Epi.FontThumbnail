using Geta.Epi.FontThumbnail.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace Geta.Epi.FontThumbnail.Tests
{
    public class ThumbnailIconControllerTests : IClassFixture<ThumbnailIconControllerFixture>
    {
        private readonly ThumbnailIconControllerFixture fixture;

        public ThumbnailIconControllerTests(ThumbnailIconControllerFixture fixture)
        {
            this.fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome), "fontawesome.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Brands), "fa-brands-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Regular), "fa-regular-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Solid), "fa-solid-900.ttf")]
        public void GenerateThumbnail_FromEmbedded(int icon, string embeddedFont)
        {
            // Arrange
            fixture.settings.Character = icon;
            fixture.settings.EmbeddedFont = embeddedFont;

            // Act
            var result = fixture.controller.GenerateThumbnail(fixture.settings) as ImageResult;

            // Assert
            Assert.NotNull(result?.Image);
            Assert.True(GetUniqueImageColors(result.Image).Count() > 1, "Image is blank.");
        }

        [Theory]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome), "fontawesome.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Brands), "fa-brands-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Regular), "fa-regular-400.ttf")]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Solid), "fa-solid-900.ttf")]
        public void GenerateThumbnail_FromDisk(int icon, string customFont)
        {
            // Arrange
            fixture.settings.Character = icon;
            fixture.settings.CustomFontName = customFont;

            // Act
            var result = fixture.controller.GenerateThumbnail(fixture.settings) as ImageResult;

            // Assert
            Assert.NotNull(result?.Image);
            Assert.True(GetUniqueImageColors(result.Image).Count() > 1, "Image is blank.");
        }

        public static IEnumerable<object[]> GetEnumValues(Type type, string fileName)
        {
            foreach (var icon in Enum.GetValues(type))
            {
                yield return new object[] { icon, fileName };
            }
        }

        private static IEnumerable<Color> GetUniqueImageColors(Image image)
        {
            using (var bitmap = new Bitmap(image))
            {
                var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
                var bitmapBytes = new byte[bitmapData.Width * bitmapData.Height * 3];
                var colors = new Color[bitmapData.Width * bitmapData.Height];
                Marshal.Copy(bitmapData.Scan0, bitmapBytes, 0, bitmapBytes.Length);
                bitmap.UnlockBits(bitmapData);

                for (int i = 0; i < colors.Length; i++)
                {
                    var start = i * 3;
                    colors[i] = Color.FromArgb(bitmapBytes[start], bitmapBytes[start + 1], bitmapBytes[start + 2]);
                }

                return colors.Distinct();
            }
        }
    }
}
