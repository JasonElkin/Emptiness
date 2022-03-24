using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Extensions;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class NullableIntegerConverter : IntegerValueConverter, IEmptinessPropertyValueConverter
    {
        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) => typeof(int?);

        public override object? ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview)
        {
            return source.TryConvertTo<int?>().Result;
        }
    }
}
