using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using EPiServer.Framework.TypeScanner;
using EPiServer.Shell.Modules;

namespace Geta.Epi.FontThumbnail
{
	public class FontThumbnailModule : ShellModule
	{
		public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath) : base(name, routeBasePath, resourceBasePath)
		{
		}

		public FontThumbnailModule(string name, string routeBasePath, string resourceBasePath, ITypeScannerLookup typeScannerLookup, VirtualPathProvider virtualPathProvider) : base(name, routeBasePath, resourceBasePath, typeScannerLookup, virtualPathProvider)
		{
		}
	}
}