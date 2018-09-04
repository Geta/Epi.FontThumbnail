# Geta.Epi.FontThumbnail

## Description
This package basically consists of an override to the built in "ImageUrlAttribute" that is used to specify preview images for the different contenttypes in your Episerver project. The only difference is that with this attribute the images are generated using a configured background color, foreground color and a reference to a FontAwesome icon.n

![Screenshot of package](/docs/fontthumbnail_overview.jpg)

## Features
* Generates preview images for the different contenttypes in your Episerver project
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

## More info
https://getadigital.com/blog/contenttype-preview-images-w.-icons/

https://getadigital.com/blog/new-version-of-fontthumbnail/

## Package maintainer
https://github.com/degborta

## Contributors
https://github.com/johanbenschop
