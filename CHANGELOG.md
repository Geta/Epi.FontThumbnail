# Changelog

All notable changes to this project will be documented in this file.

## [1.2.4]

### Changed
- Updated Font Awesome to release 5.13.0

## [1.2.3]

### Changed
- Added some more tests for using custom fonts
- Added XML docs, mainly for the attributes (ThumbnailAttribute and TreeIconAttribute)

## [1.2.0]

### Changed
- Updated FontAwesome to release 5.12.1
- Added custom authorize group "ThumbnailGroup"
- Security update
- New default background color
- Added more detailed documentation

## [1.1.5]

### Changed
- Added support for custom tree icons, thanks to https://github.com/johanbenschop

## [1.1.4]

### Changed
- Update Font Awesome to release 5.7.1.

## [1.1.3]

### Changed
- Update Font Awesome to release 5.6.3
- Update readme

## [1.1.2]

### Changed
- Added support for Font Awesome version 5.3.0.

## [1.0.9]

### Changed
- Episerver 11 update and changed web project to a class libary.

## [1.0.6]

### Changed
- Bugfix, issue with locked custom fonts
- Added support for loading custom fonts from disk

## [1.0.4]

### Changed
- Updated FontAwesome enum with support for all 4.7 fonts

## [1.0.3]

### Changed
- Bugfix: Changed to loading font from embedded resource instead of from file module, to prevent locking of font file.
- Bugfix: Changed to working with cloned image to prevent locking of generated thumbnails

## [1.0.2]

### Changed
- Initial release







































## [3.0.0]

### Changed

- Upgraded to Episerver Commerce 13.

## [2.0.13]

### Changed

- Fix for shipment amount validation issue when updating shipping option [#63](https://github.com/Geta/Klarna/pull/63)

## [2.0.12]

### Changed

- Use PricesIncludeTax property on market to determine if tax should be included on orderline

## [2.0.11]

### Changed

- [Klarna Order management] Exception handling when order can not be retrieved

## [2.0.10]

### Changed

- Using primary host as a site URL with fallback to site URL.

## [2.0.8]

### Changed

- Fixed discount calculation being wrong.

## [2.0.7]

### Changed

- Fixed a bug when shipping tax was not calculated properly for markets which has "PriceIncludeTax" setting.

## [2.0.6]

### Changed

- Fixed a bug when tax was not calculated properly for markets which has "PriceIncludeTax" setting.

## [2.0.5]

### Changed

- Added mapping from language NO to NB to make the widget render in norwegian.

## [2.0.4]

### Changed

- Fixed shipment option loading by language.

## [2.0.3]

### Changed

- Update System.Security.Cryptography.Xml to version="4.4.2" (security vulnerabilities)

## [2.0.2]

### Changed

- [Klarna Checkout] Made KlarnaCheckoutService overrideable, changed functions to virtual.

## [2.0.1]

### Changed

- [Klarna Checkout] Use market default languange when loading settings from Commerce Manager

## [2.0.0]

### Added

- Initial release to Episerver nuget
- Added changelog file

### Changed

- Added ".v3" to all package names to prevent issues with existing nuget packages on the official nuget feed
