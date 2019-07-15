using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using EPiServer.Web;
using EPiServer.ServiceLocation;

namespace Geta.Epi.FontThumbnail.ResourceProvider
{
    internal class AssemblyResourceProvider : VirtualPathProvider
    {
        private static string _rootPath;
        private static Assembly _assembly;

        public static string ModuleRootPath
        {
            get
            {
                if (_rootPath == null)
                {
                    var rootPath = EPiServer.Shell.Configuration.EPiServerShellSection.GetSection().ProtectedModules.RootPath;
                    _rootPath = rootPath + Constants.ModuleName + "/";
                }
                return _rootPath;
            }
            protected set => _rootPath = value;
        }

        public static Assembly ModuleAssembly
        {
            get => _assembly ?? (_assembly = typeof(AssemblyResourceProvider).Assembly);
            protected set => _assembly = value;
        }

        public static string GetResourcePath(string path)
        {
            var checkPath = ServiceLocator.Current.GetInstance<IVirtualPathResolver>().ToAppRelative(path);

            if (checkPath.StartsWith(ModuleRootPath, StringComparison.OrdinalIgnoreCase))
            {
                var relative = checkPath.Substring(ModuleRootPath.Length);

                if (relative.StartsWith("ClientResources/", StringComparison.OrdinalIgnoreCase)
                    || relative.Equals("module.config", StringComparison.OrdinalIgnoreCase))
                {
                    return ModuleAssembly.GetName().Name + "." + relative.Replace('/', '.');
                }
            }

            return null;
        }

        private static bool IsAppResourcePath(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return false;

            try
            {
                var virtualPathResolver = ServiceLocator.Current.GetInstance<IVirtualPathResolver>();
                var checkPath = virtualPathResolver.ToAppRelative(virtualPath);
                var appResourcePath = checkPath.StartsWith(ModuleRootPath, StringComparison.OrdinalIgnoreCase);
                return appResourcePath;
            }
            catch (HttpException)
            {
                return false;
            }
        }

        private static bool IsAppResourceDir(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return false;

            try
            {
                var virtualPathResolver = ServiceLocator.Current.GetInstance<IVirtualPathResolver>();
                var checkPath = virtualPathResolver.ToAppRelative(virtualPath);
                return checkPath.EndsWith("/") && checkPath.StartsWith(ModuleRootPath, StringComparison.OrdinalIgnoreCase);
            }
            catch (HttpException)
            {
                return false;
            }
        }

        public override bool FileExists(string virtualPath)
        {
            if (IsAppResourcePath(virtualPath))
            {
                var resourcePath = GetResourcePath(virtualPath);

                return ModuleAssembly.GetManifestResourceNames().Any(r => r.Equals(resourcePath, StringComparison.OrdinalIgnoreCase));
            }

            return Previous?.FileExists(virtualPath) == true;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            return IsAppResourcePath(virtualPath) ? new AssemblyResourceVirtualFile(virtualPath) : Previous?.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart)
        {
            return IsAppResourcePath(virtualPath) ? new NeverExpiresCacheCacheDependency() : base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        public override string GetFileHash(string virtualPath, IEnumerable virtualPathDependencies)
        {
            if (IsAppResourcePath(virtualPath))
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(GetLocalFileHash(virtualPath));
                if (virtualPathDependencies != null)
                {
                    foreach (var path in virtualPathDependencies.OfType<string>().OrderBy(x => x, StringComparer.OrdinalIgnoreCase))
                    {
                        stringBuilder.Append(!IsAppResourcePath(path)
                            ? base.GetFileHash(path, new[] {path})
                            : GetLocalFileHash(path));
                    }
                }

                foreach (var module in ModuleAssembly.GetLoadedModules().OrderBy(m => m.Name, StringComparer.OrdinalIgnoreCase))
                {
                    stringBuilder.Append(module.ModuleVersionId.ToString());
                }

                return stringBuilder.ToString().GetHashCode().ToString("x", CultureInfo.InvariantCulture);
            }

            return Previous.GetFileHash(virtualPath, virtualPathDependencies);
        }

        private static string GetLocalFileHash(string virtualPath)
        {
            var stringBuilder = new StringBuilder();
            const string fileHash = "9201";
            stringBuilder.Append(fileHash);
            var version = ModuleAssembly.GetName().Version.ToString();
            stringBuilder.Append(virtualPath);
            stringBuilder.Append(version);
            return stringBuilder.ToString().GetHashCode().ToString("x", CultureInfo.InvariantCulture);
        }

        public override VirtualDirectory GetDirectory(string virtualDir)
        {
            return IsAppResourceDir(virtualDir) ? new AssemblyResourceVirtualDirectory(virtualDir) : Previous.GetDirectory(virtualDir);
        }

        public override bool DirectoryExists(string virtualDir)
        {
            return IsAppResourceDir(virtualDir) || Previous.DirectoryExists(virtualDir);
        }
    }
}
