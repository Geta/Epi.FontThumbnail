using Geta.Epi.FontThumbnail.Controllers;
using Geta.Epi.FontThumbnail.Mvc;
using System;
using System.Configuration;
using System.Drawing;
using System.Linq;
using Xunit;

namespace Geta.Epi.FontThumbnail.Tests
{
    public class ThumbnailIconControllerTest
    {
        public ThumbnailIconControllerTest()
        {
            ConfigurationManager.AppSettings["FontThumbnail.CachePath"] = @"C:\Dev\TestOutput\";
        }

        [Fact]
        public void TestGenerateThumbnail_FontAwesome5Brands()
        {
            // Arrange
            var service = new FontThumbnailService();
            var controller = new ThumbnailIconController(service);
            var values = (FontAwesome5Brands[])Enum.GetValues(typeof(FontAwesome5Brands));
            var settings = new Settings.ThumbnailSettings
            {
                FontSize = Constants.DefaultFontSize,
                BackgroundColor = Constants.DefaultBackgroundColor,
                ForegroundColor = Constants.DefaultForegroundColor,
                Height = Constants.DefaultHeight,
                Width = Constants.DefaultWidth,
                EmbeddedFont = "fa-brands-400.ttf"
            };

            // Act
            foreach (var item in values)
            {
                settings.Character = (int)item;
                //var image = service.GenerateImage(settings);
                var result = controller.GenerateThumbnail(settings) as ImageResult;
            }
        }

        [Fact]
        public void TestGenerateThumbnail_FontAwesome5Regular()
        {
            // Arrange
            var service = new FontThumbnailService();
            var controller = new ThumbnailIconController(service);
            var values = (FontAwesome5Regular[])Enum.GetValues(typeof(FontAwesome5Regular));
            var settings = new Settings.ThumbnailSettings
            {
                FontSize = Constants.DefaultFontSize,
                BackgroundColor = Constants.DefaultBackgroundColor,
                ForegroundColor = Constants.DefaultForegroundColor,
                Height = Constants.DefaultHeight,
                Width = Constants.DefaultWidth,
                EmbeddedFont = "fa-regular-400.ttf"
            };

            // Act
            foreach (var item in values)
            {
                settings.Character = (int)item;
                //var image = service.GenerateImage(settings);
                var result = controller.GenerateThumbnail(settings) as ImageResult;
            }
        }

        [Fact]
        public void TestGenerateThumbnail_FontAwesome5Solid()
        {
            // Arrange
            var service = new FontThumbnailService();
            var controller = new ThumbnailIconController(service);
            var values = (FontAwesome5Solid[])Enum.GetValues(typeof(FontAwesome5Solid));
            var settings = new Settings.ThumbnailSettings
            {
                FontSize = Constants.DefaultFontSize,
                BackgroundColor = Constants.DefaultBackgroundColor,
                ForegroundColor = Constants.DefaultForegroundColor,
                Height = Constants.DefaultHeight,
                Width = Constants.DefaultWidth,
                EmbeddedFont = "fa-solid-900.ttf"
            };

            // Act
            foreach (var item in values)
            {
                settings.Character = (int)item;
                //var image = service.GenerateImage(settings);
                var result = controller.GenerateThumbnail(settings) as ImageResult;
            }
        }
    }
}
