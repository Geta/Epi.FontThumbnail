using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Hosting;

namespace Geta.Epi.FontThumbnail.ResourceProvider
{
    internal class AssemblyResourceProviderRegistrationModule : IVirtualPathProviderModule
    {
        public IEnumerable<VirtualPathProvider> CreateProviders(InitializationEngine context)
        {
            return context.HostType.Equals(HostType.WebApplication)
                ? new VirtualPathProvider[] { new AssemblyResourceProvider() } : Enumerable.Empty<VirtualPathProvider>();
        }
    }
}
