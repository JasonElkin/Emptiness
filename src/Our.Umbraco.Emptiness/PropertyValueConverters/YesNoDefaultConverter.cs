using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;

namespace Our.Umbraco.Emptiness.PropertyValueConverters
{
    public class YesNoDefaultConverter : YesNoValueConverter, IEmptinessPropertyValueConverter
    {
        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview)
        {
            // in xml a boolean is: string
            // in the database a boolean is: string "1" or "0" or empty
            // typically the converter does not need to handle anything else ("true"...)
            // however there are cases where the value passed to the converter could be a non-string object, e.g. int, bool

            if (source is string s)
            {
                if (s == "0")
                    return false;

                if (s == "1")
                    return true;

                if(bool.TryParse(s, out bool result))
                    return result;
            }

            if (source is int intVal)
            {
                if (intVal == 1)
                {
                    return true;
                }
                if (intVal == 0)
                {
                    return false;
                }
            }

            // this is required for correct true/false handling in nested content elements
            if (source is long longVal)
            {
                if (longVal == 1)
                {
                    return true;
                }
                if (longVal == 0)
                {
                    return false;
                }
            }

            if (source is bool boolean)
                return boolean;

            var config = propertyType.DataType.ConfigurationAs<TrueFalseConfiguration>();

            var defaultValue = config.Default;

            return defaultValue;
        }
    }
}
