# Geta.Epi.FontThumbnail

This package basically consists of an override to the built in "ImageUrlAttribute" that is used to specify preview images for the different contenttypes in your Episerver project. The only difference is that with this attribute the images are generated using a configured background color, foreground color and a reference to a FontAwesome icon.

![Screenshot of package](/docs/fontthumbnail_overview.jpg)

## How to use

Using the built in ImageUrlAttribute, you specify the images to be presented like this:
```cs
[ImageUrlAttribute("~/images/contenttypes/articlepage.png")]
```

Using this package you can specify it like this instead.
```cs
[ThumbnailIcon(FontAwesome.Github)]
```

or with overriddes for specifying different colors and size
```cs
[ThumbnailIcon(FontAwesome.Github,"#000000","#ffffff",40)]
```
The defaults if nothing else is specified is of course the Geta colors as seen in the screenshot.

The images that gets generated is cached in [appDataPath]\thumb_cache\

Using the following configuration you can change the default colors:
```xml
<appSettings>
    <add key="FontThumbnail.BackgroundColor" value="#000000" />
    <add key="FontThumbnail.ForegroundColor" value="#ffffff" />
    <add key="FontThumbnail.FontSize" value="40" />
</appSettings>
```

## How to install

Install NuGet package (use [EPiServer Nuget](http://nuget.episerver.com))

    Install-Package Geta.Epi.FontThumbnail