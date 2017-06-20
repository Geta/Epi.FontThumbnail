using System.IO;
using System.Web.Mvc;
using System.Web.Routing;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web;

namespace Geta.Epi.FontThumbnail
{
    [ModuleDependency(typeof(EPiServer.Cms.Shell.InitializableModule))]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    [InitializableModule]
    public class FontThumbnailInitialization : IInitializableModule
    {
        private static bool _initialized;

        public void Initialize(InitializationEngine context)
        {
            if (_initialized || context.HostType != HostType.WebApplication)
            {
                return;
            }

            // the route for the controller responsible for generating or loading the image from disk
            RouteTable.Routes.MapRoute("ThumbnailIcon", Constants.UrlFragment, new { controller = "ThumbnailIcon", action = "GenerateThumbnail"});

            // verify cache directory exists
            string fullPath = VirtualPathUtilityEx.RebasePhysicalPath(Constants.DefaultCachePath);
            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }

            _initialized = true;
        }

        public void Uninitialize(InitializationEngine context)
        {
        }

        public void Preload(string[] parameters)
        {
        }
    }
}