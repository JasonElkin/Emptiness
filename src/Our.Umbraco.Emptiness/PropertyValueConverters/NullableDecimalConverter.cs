using System;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableDecimalConverter : IPropertyValueConverter
    {
        readonly IPropertyValueConverter coreConverter = new DecimalValueConverter();

        public Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(decimal?);

        public object? ConvertSourceToIntermediate(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            object? source,
            bool preview)
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

        public bool IsConverter(IPublishedPropertyType propertyType)
            => coreConverter.IsConverter(propertyType);

        public bool? IsValue(object? value, PropertyValueLevel level)
            => coreConverter.IsValue(value, level);

        public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType)
            => coreConverter.GetPropertyCacheLevel(propertyType);

        public object? ConvertIntermediateToObject(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel,
            object? inter,
            bool preview)
        {
            return coreConverter.ConvertIntermediateToObject(
                owner,
                propertyType,
                referenceCacheLevel,
                inter,
                preview);
        }

        public object? ConvertIntermediateToXPath(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel,
            object? inter,
            bool preview)
        {
            return coreConverter.ConvertIntermediateToXPath(
                owner,
                propertyType,
                referenceCacheLevel,
                inter,
                preview);
        }
    }
}
