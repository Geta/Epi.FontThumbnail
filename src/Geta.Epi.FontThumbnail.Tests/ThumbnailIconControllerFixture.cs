using System;
using System.Configuration;
using System.IO;
using EPiServer.Web;
using Geta.Epi.FontThumbnail.Controllers;
using Geta.Epi.FontThumbnail.Settings;

namespace Geta.Epi.FontThumbnail.Tests
{
    public class ThumbnailIconControllerFixture : IDisposable
    {
        public readonly ThumbnailIconController controller;
        public readonly ThumbnailSettings settings;
        private readonly string _temporaryDirectory;

        public ThumbnailIconControllerFixture()
        {
            var partialDirectpry = $"[appDataPath]\\thumb_cache\\{Guid.NewGuid()}\\";
            ConfigurationManager.AppSettings["FontThumbnail.CachePath"] = partialDirectpry;
            _temporaryDirectory = VirtualPathUtilityEx.RebasePhysicalPath(partialDirectpry);

            Directory.CreateDirectory(_temporaryDirectory);

            var service = new FontThumbnailService();
            controller = new ThumbnailIconController(service);
            settings = new ThumbnailSettings
            {
                FontSize = Constants.DefaultFontSize,
                BackgroundColor = Constants.DefaultBackgroundColor,
                ForegroundColor = Constants.DefaultForegroundColor,
                Height = Constants.DefaultHeight,
                Width = Constants.DefaultWidth
            };
        }

        public void Dispose()
        {
            Directory.Delete(_temporaryDirectory, true);
        }
    }
}
