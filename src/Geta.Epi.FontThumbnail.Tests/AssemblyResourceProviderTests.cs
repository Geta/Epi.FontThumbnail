using System;
using System.Collections.Generic;
using System.IO;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Internal;
using Geta.Epi.FontThumbnail.ResourceProvider;
using Moq;
using Xunit;

namespace Geta.Epi.FontThumbnail.Tests
{
    public class AssemblyResourceProviderFixture
    {
        public AssemblyResourceProviderFixture()
        {
            var virtualPathResolver = new DefaultVirtualPathResolver(WebHostingEnvironment.Instance);
            var mockServiceLocator = new Mock<IServiceLocator>();
            mockServiceLocator.Setup(i => i.GetInstance<IVirtualPathResolver>()).Returns(virtualPathResolver);

            ServiceLocator.SetLocator(mockServiceLocator.Object);

            EPiServer.Shell.Configuration.EPiServerShellSection.GetSection().ProtectedModules.RootPath = "~/EPiServer/";
        }
    }

    public class AssemblyResourceProviderTests : IClassFixture<AssemblyResourceProviderFixture>
    {
        [Fact]
        public void GetCacheKey_ShouldReturnNull()
        {
            // Arrange
            var provider = new AssemblyResourceProvider();
            const string virtualPath = "~/EPiServer/Geta.Epi.FontThumbnail/ClientResources/css/all.min.css";

            // Act
            var result = provider.GetCacheKey(virtualPath);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCacheDependency_ShouldNeverExpire()
        {
            // Arrange
            var provider = new AssemblyResourceProvider();
            const string virtualPath = "~/EPiServer/Geta.Epi.FontThumbnail/ClientResources/fa5/css/all.min.css";

            // Act
            var result = provider.GetCacheDependency(virtualPath, new string[0], DateTime.Now);

            // Assert
            Assert.IsType<NeverExpiresCacheCacheDependency>(result);
            Assert.False(result.HasChanged);
            Assert.Equal(new DateTime(), result.UtcLastModified);
        }

        [Fact]
        public void GetFileHash_ShouldWork()
        {
            // Arrange
            var provider = new AssemblyResourceProvider();

            // Act
            var result = provider.GetFileHash("~/EPiServer/Geta.Epi.FontThumbnail/ClientResources/fa5/css/all.min.css", new string[0]);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("b03f3a2c".Length, result.Length);
        }

        [Theory]
        [MemberData(nameof(GetFilesInClientResourcesFolder))]
        public void FilesInClientResourcesFolder_ShouldExist(string path)
        {
            // Arrange
            var provider = new AssemblyResourceProvider();

            // Act
            var result = provider.FileExists("~/EPiServer/Geta.Epi.FontThumbnail/ClientResources" + path);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(GetFilesInClientResourcesFolder))]
        public void FilesInClientResourcesFolder_ShouldLoad(string path)
        {
            // Arrange
            var provider = new AssemblyResourceProvider();

            // Act
            var result = provider.GetFile("~/EPiServer/Geta.Epi.FontThumbnail/ClientResources" + path);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("~/this-file-should-not-exist.ttf")]
        [InlineData("~/EPiServer/Geta.Epi.FontThumbnail/ClientResources/nor-this-file.woff2")]
        [InlineData("~/EPiServer/Geta.Epi.FontThumbnail/ClientResources/webfonts/and-not-this-file-too.woff2")]
        public void FilesNotInClientResourcesFolder_ShouldNotExist(string path)
        {
            // Arrange
            var provider = new AssemblyResourceProvider();

            // Act
            var result = provider.FileExists(path);

            // Assert
            Assert.False(result);
        }

        public static IEnumerable<object[]> GetFilesInClientResourcesFolder()
        {
            const string basePath = "../../../../Geta.Epi.FontThumbnail/ClientResources";
            var allFiles = Directory.GetFiles(basePath, "*.*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
                yield return new[] {file.Substring(basePath.Length).Replace("\\", "/")};
        }
    }
}
