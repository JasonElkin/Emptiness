<img src="docs/img/logo.svg" width="200" height="200" align="right"/>

# Emptiness
[![.NET](https://github.com/JasonElkin/Emptiness/actions/workflows/build.yml/badge.svg)](https://github.com/JasonElkin/Emptiness/actions/workflows/build.yml)

 Nullable Property Value Converters for value types in Umbraco.

### Why would I want that?

If an editor hasn't set a value for a "value type" property, then these converters will return null instead of a default value. This makes editor intent clearer - you can tell the difference between an integer that an edtor intentionally chose to be "0" as opposed to an integer property that's been left empty.

## The Converters

### Nullable Property Value Converters

The following are enabled by default, and all return null if an editor has not entered a value.

 - NullableDatePickerConverter
 - NullableDecimalConverter
 - NullableIntegerConverter
 - NullableLabelConverter
### Property Value Converters for Toggles (True/False)
Though they don't have a null state in the UI, they can be empty if:

  1. A property has been added to a content type, but some content of that type has not been (re)published since.
  2. Content has been created via the API, without the property value having been set.

In these cases the following converters might be helpful...
#### YesNoDefaultConverter
Enabled by default. This converter returns the "Initial State" (default) value that has been configured in the property editor's settings.

#### NullableYesNoConverter
Disabled by default.

## License(s) & Copyright

Copyright &copy; 2022 Jason Elkin

Licensed under the [MIT License](LICENSE.md).

 <a href='https://www.freepik.com/vectors/crate'>Crate image created by vectorpocket - www.freepik.com</a>
