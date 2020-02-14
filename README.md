# Geta.Epi.FontThumbnail

## Description
This package basically consists of an override to the built in "ImageUrlAttribute" that is used to specify preview images for the different contenttypes in your Episerver project. The only difference is that with this attribute the images are generated using a configured background color, foreground color and a reference to a FontAwesome icon. Since version [1.1.5] another feature was introduced where the same configured icon will also replace the treeicon in the page tree (feature needs to be explicitly turned on using configuration). Supports Episerver 11.x

![Screenshot of package](/docs/fontthumbnail_overview.jpg)

## Features
* Generates preview images for the different contenttypes in your Episerver project
* Replace tree icons with custom icons for content types
* Support for using Font Awesome Free 5 and 4 icons
* Supports customized foreground and background color on generated images
* Loading custom fonts

## How to get started?
* Install NuGet package (use [EPiServer Nuget](http://nuget.episerver.com))
* ``Install-Package Geta.Epi.FontThumbnail``


## Details
Using the built in ImageUrlAttribute, you specify the images to be presented like this:
```cs
[ImageUrlAttribute("~/images/contenttypes/articlepage.png")]
```

Using this package you can specify it like this instead:
```cs
[ThumbnailIcon(FontAwesome5Brands.Github)]
```
There are a couple different enum types available: `FontAwesome5Brands`, `FontAwesome5Regular` and `FontAwesome5Solid` for the different Font Awesome 5 styles. There is also the `FontAwesome` enum for the Font Awesome version 4 icons. 

Or with overriddes for specifying different colors and size:
```cs
[ThumbnailIcon(FontAwesome5Brands.Github,"#000000","#ffffff",40)]
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

Loading custom fonts
To load custom icon fonts you can place the font you want to use in the default folder [appDataPath]\fonts\ (this can also be customized using appSettings.
```xml
<appSettings>
    <add key="FontThumbnail.CustomFontPath" value="" />
</appSettings>
```
Then specify the font and the character to use in the ThumbnailIcon constructor like this.
```cs
[ThumbnailIcon("fontello.ttf",0xe801)]
```

## Tree icon feature

![Screenshot of package](/docs/treeicon_overview.jpg)

To enable the feature to use custom icons in the content tree you have to add this configuration:
```xml
<appSettings>
    <add key="FontThumbnail.EnableTreeIcons" value="true"/>
</appSettings>
```

By default the same icons will be used in the content tree if you have defined an icon using the ThumbnailIcon-attribute.
```cs
[ThumbnailIcon(FontAwesome5Brands.Github)]
```

You can however disable this for specific content types using the ignore property on the TreeIcon-attribute on the content type, like this:

```cs
[ContentType(DisplayName = "Blog List Page")]
[ThumbnailIcon(FontAwesome5Solid.Blog)]
[TreeIcon(Ignore = true)]
public class BlogListPage : FoundationPageData
{
    ...
}
```

There is also support for overriding the icon defined in the ThumbnailIcon-attribute like this:
```cs
[ContentType(DisplayName = "Blog List Page")]
[ThumbnailIcon(FontAwesome5Solid.Blog)]
[TreeIcon(FontAwesome5Solid.CheckDouble)]
public class BlogListPage : FoundationPageData
{
    ...
}
```

or with the optional rotation (FlipVertical, FlipHorizontal, Rotate90, Rotate180, Rotate270):
```cs
[TreeIcon(FontAwesome5Solid.CheckDouble, Rotations.Rotate90)]
```

## Changelog

[Changelog](CHANGELOG.md)


## More info
https://getadigital.com/blog/contenttype-preview-images-w.-icons/

https://getadigital.com/blog/new-version-of-fontthumbnail/

## Package maintainer
https://github.com/degborta

## Contributors
https://github.com/johanbenschop
