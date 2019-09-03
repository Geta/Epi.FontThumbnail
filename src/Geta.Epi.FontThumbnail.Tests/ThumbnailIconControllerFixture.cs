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
        internal readonly ThumbnailIconController Controller;
        internal readonly ThumbnailSettings Settings;
        private readonly string _temporaryDirectory;

        public ThumbnailIconControllerFixture()
        {
            var partialDirectory = $"[appDataPath]\\thumb_cache\\{Guid.NewGuid()}\\";
            ConfigurationManager.AppSettings["FontThumbnail.CachePath"] = partialDirectory;
            _temporaryDirectory = VirtualPathUtilityEx.RebasePhysicalPath(partialDirectory);

            Directory.CreateDirectory(_temporaryDirectory);

            var service = new FontThumbnailService();
            Controller = new ThumbnailIconController(service);
            Settings = new ThumbnailSettings
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
