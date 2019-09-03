using System;
using System.Configuration;
using EPiServer.Core;
using EPiServer.Shell;
using Geta.Epi.FontThumbnail.Initialization;
using Geta.Epi.FontThumbnail.Tests.Models;
using Xunit;

namespace Geta.Epi.FontThumbnail.Tests
{
    public class TreeIconUIDescriptorInitializationTests : IDisposable
    {
        [Fact]
        public void Enabled_PageWithoutThumbnailIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithoutThumbnailIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithoutThumbnailIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithoutThumbnailIcon>(globallyEnabled: false,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyThumbnailIcon_InUse()
        {
            // Arrange
            Setup<PageWithOnlyThumbnailIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-road fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithOnlyThumbnailIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithOnlyThumbnailIcon>(globallyEnabled: false,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndTreeIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-anchor fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithThumbnailIconAndTreeIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndTreeIcon>(globallyEnabled: false,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndTreeIconOnIgnore_NotInUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndTreeIconOnIgnore>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Disabled_PageWithThumbnailIconAndTreeIconOnIgnore_NotInUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndTreeIconOnIgnore>(globallyEnabled: false,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyTreeIconWithoutIcon_NotInUse()
        {
            // Arrange
            Setup<PageWithOnlyTreeIconWithoutIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Null(descriptor.IconClass);
            Assert.False(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithOnlyTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithOnlyTreeIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-road fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndDifferentTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndDifferentTreeIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("far fa-clock fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_MediaDataWithOnlyThumbnailIcon_InUse()
        {
            // Arrange
            Setup<MediaDataWithOnlyThumbnailIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-images fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_ImageDataWithOnlyThumbnailIcon_InUse()
        {
            // Arrange
            Setup<ImageDataWithOnlyThumbnailIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-image fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndInheritedTreeIcon_InUse()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndInheritedTreeIcon>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.Equal("fas fa-box-open fa-fw", descriptor.IconClass);
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndRotation_Should_Rotate()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndRotation>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
            Assert.Contains("fa-rotate-180", descriptor.IconClass);
        }

        [Fact]
        public void Enabled_PageWithTreeIconAmdRotation_Should_Rotate()
        {
            // Arrange
            Setup<PageWithTreeIconAmdRotation>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
            Assert.Contains("fa-rotate-90", descriptor.IconClass);
        }

        [Fact]
        public void Enabled_PageWithThumbnailIconAndDifferentTreeIconRotation_TreeIcon_Should_TakePrecedence()
        {
            // Arrange
            Setup<PageWithThumbnailIconAndDifferentTreeIconRotation>(globallyEnabled: true,
                out var initializableModule, out var descriptor);

            // Act
            initializableModule.EnrichDescriptorWithIconClass(descriptor);

            // Assert
            Assert.True(TreeIconUIDescriptorInitialization.EnabledAndInUse);
            Assert.Contains("fa-flag", descriptor.IconClass);
            Assert.Contains("fa-rotate-90", descriptor.IconClass);
        }

        private static void Setup<TType>(bool globallyEnabled, out TreeIconUIDescriptorInitialization initializableModule, out UIDescriptor descriptor) where TType : IContent
        {
            ConfigurationManager.AppSettings[Constants.AppSettings.EnableTreeIcons] = globallyEnabled.ToString();
            initializableModule = new TreeIconUIDescriptorInitialization();
            descriptor = new UIDescriptor(typeof(TType));
        }

        public void Dispose()
        {
            TreeIconUIDescriptorInitialization.EnabledAndInUse = false;
        }
    }
}
