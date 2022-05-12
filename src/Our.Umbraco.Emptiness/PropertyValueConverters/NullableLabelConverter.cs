using System;
using System.Globalization;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableLabelConverter : IPropertyValueConverter
    {
        readonly IPropertyValueConverter coreConverter = new LabelValueConverter();

        public Type GetPropertyValueType(IPublishedPropertyType propertyType)
        {
            var valueType = ConfigurationEditor.ConfigurationAs<LabelConfiguration>(propertyType.DataType.Configuration);
            return valueType?.ValueType switch
            {
                ValueTypes.DateTime or ValueTypes.Date => typeof(DateTime?),
                ValueTypes.Time => typeof(TimeSpan?),
                ValueTypes.Decimal => typeof(decimal?),
                ValueTypes.Integer => typeof(int?),
                ValueTypes.Bigint => typeof(long?),
                _ => typeof(string),
            };
        }

        public object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object? source, bool preview)
        {
            var valueType = ConfigurationEditor.ConfigurationAs<LabelConfiguration>(propertyType.DataType.Configuration);

            switch (valueType?.ValueType)
            {
                case ValueTypes.DateTime:
                case ValueTypes.Date:
                    if (source is DateTime sourceDateTime)
                        return sourceDateTime;
                    if (source is string sourceDateTimeString)
                        return DateTime.TryParse(sourceDateTimeString, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt) ? dt : null;
                    return null;
                case ValueTypes.Time:
                    if (source is DateTime sourceTime)
                        return sourceTime.TimeOfDay;
                    if (source is string sourceTimeString)
                        return TimeSpan.TryParse(sourceTimeString, CultureInfo.InvariantCulture, out var ts) ? ts : null;
                    return null;
                case ValueTypes.Decimal:
                    if (source is decimal sourceDecimal) return sourceDecimal;
                    if (source is string sourceDecimalString)
                        return decimal.TryParse(sourceDecimalString, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? d : null;
                    if (source is double sourceDouble)
                        return Convert.ToDecimal(sourceDouble);
                    return null;
                case ValueTypes.Integer:
                    if (source is int sourceInt) return sourceInt;
                    if (source is string sourceIntString)
                        return int.TryParse(sourceIntString, NumberStyles.Integer, CultureInfo.InvariantCulture, out var i) ? i : null;
                    return null;
                case ValueTypes.Bigint:
                    if (source is string sourceLongString)
                        return long.TryParse(sourceLongString, out var i) ? i : null;
                    return null;
                default: // everything else is a string
                    return source?.ToString() ?? string.Empty;
            }
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
