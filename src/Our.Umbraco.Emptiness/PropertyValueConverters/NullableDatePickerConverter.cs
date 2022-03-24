using System;
using System.Xml;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableDatePickerConverter : DatePickerValueConverter, IEmptinessPropertyValueConverter
    {
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(DateTime?);

        public override object? ConvertSourceToIntermediate(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            object source,
            bool preview)
        {

            if (source is string sourceString)
            {
                var attempt = sourceString.TryConvertTo<DateTime?>();
                return attempt.Success == false ? null : attempt.Result;
            }

            return source as DateTime?;
        }

        public override object? ConvertIntermediateToXPath(
            IPublishedElement owner,
            IPublishedPropertyType propertyType,
            PropertyCacheLevel referenceCacheLevel,
            object inter,
            bool preview)
        {
            var interDate = inter as DateTime?;

            if (interDate is null)
            {
                return null;
            }

            return XmlConvert.ToString(interDate.Value, XmlDateTimeSerializationMode.Unspecified);
        }
    }
}
