using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableIntegerConverter : IPropertyValueConverter
    {
        readonly IPropertyValueConverter coreConverter = new IntegerValueConverter();

        public Type GetPropertyValueType(IPublishedPropertyType propertyType)
            => typeof(int?);

        public object? ConvertSourceToIntermediate(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            object? source,
            bool preview)
        {
            return source.TryConvertTo<int?>().Result;
        }

        public bool IsConverter(IPublishedPropertyType propertyType)
        {
            return coreConverter.IsConverter(propertyType);
        }

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
            return coreConverter.ConvertIntermediateToXPath(owner,
                propertyType,
                referenceCacheLevel,
                inter,
                preview);
        }
    }
}
