using System;
using System.Xml;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableDatePickerConverter : IPropertyValueConverter
    {
        readonly IPropertyValueConverter coreConverter = new DatePickerValueConverter();

        public Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(DateTime?);

        public object? ConvertSourceToIntermediate(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            object? source,
            bool preview)
        {

            if (source is string sourceString)
            {
                var attempt = sourceString.TryConvertTo<DateTime?>();
                return attempt.Success == false ? null : attempt.Result;
            }

            return source as DateTime?;
        }

        public object? ConvertIntermediateToXPath(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel,
            object? inter,
            bool preview)
        {
            var interDate = inter as DateTime?;

            if (interDate is null)
            {
                return null;
            }

            return XmlConvert.ToString(interDate.Value, XmlDateTimeSerializationMode.Unspecified);
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
    }
}
