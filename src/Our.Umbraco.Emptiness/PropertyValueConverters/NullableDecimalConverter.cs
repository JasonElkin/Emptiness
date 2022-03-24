using System;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableDecimalConverter : DecimalValueConverter, IEmptinessPropertyValueConverter
    {
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(decimal?);

        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview)
        {
            if (source is null)
            {
                return null;
            }

            // is it already a decimal?
            if (source is decimal)
            {
                return source;
            }

            // is it a double?
            if (source is double sourceDouble)
            {
                return Convert.ToDecimal(sourceDouble);
            }

            // is it a string?
            if (source is string sourceString && decimal.TryParse(sourceString, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out decimal d))
            {
                return d;
            }

            // couldn't convert the source value - default to null
            return null;
        }
    }
}
