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
        private readonly ThumbnailIconControllerFixture _fixture;

        public ThumbnailIconControllerTests(ThumbnailIconControllerFixture fixture)
        {
            _fixture = fixture;
        }

        [Theory]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome))]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Brands))]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Regular))]
        [MemberData(nameof(GetEnumValues), typeof(FontAwesome5Solid))]
        public void GenerateThumbnail_FromEmbedded(int icon, string embeddedFont)
        {
            // Arrange
            _fixture.Settings.Character = icon;
            _fixture.Settings.EmbeddedFont = embeddedFont;

            // Act
            var result = _fixture.Controller.GenerateThumbnail(_fixture.Settings) as ImageResult;

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
            _fixture.Settings.Character = icon;
            _fixture.Settings.CustomFontName = customFont;

            // Act
            var result = _fixture.Controller.GenerateThumbnail(_fixture.Settings) as ImageResult;

            // Assert
            Assert.NotNull(result?.Image);
            Assert.True(GetUniqueImageColors(result.Image).Count() > 1, "Image is blank.");
        }

        [Theory]
        [InlineData("#FFF")]
        [InlineData("#fff")]
        [InlineData("#fff000")]
        [InlineData("#000")]
        public void CheckValidFormatHtmlColor_Valid(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = _fixture.Controller.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.True(isValid);
        }

        [Theory]
        [InlineData("#FF")]
        [InlineData("#THISISATEST")]
        [InlineData("#-132332")]
        [InlineData("#00034333")]
        [InlineData("000")]
        public void CheckValidFormatHtmlColor_Invalid(string color)
        {
            // Arrange
            var isValid = false;

            // Act
            isValid = _fixture.Controller.CheckValidFormatHtmlColor(color);

            // Assert
            Assert.False(isValid);
        }

        public static IEnumerable<object[]> GetEnumValues(Type type)
        {
            return GetEnumValues(type, null);
        }

        public static IEnumerable<object[]> GetEnumValues(Type type, string fileName)
        {
            foreach (var icon in Enum.GetValues(type))
            {
                fileName = fileName ?? ImageUrlHelper.GetEmbeddedFontLocation((Enum)icon);
                yield return new[] { icon, fileName };
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

                for (var i = 0; i < colors.Length; i++)
                {
                    var start = i * 3;
                    colors[i] = Color.FromArgb(bitmapBytes[start], bitmapBytes[start + 1], bitmapBytes[start + 2]);
                }

                return colors.Distinct();
            }
        }
    }
}
