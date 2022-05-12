using System.Linq;
using Our.Umbraco.Emptiness.PropertyValueConverters;
using Umbraco.Cms.Core.PropertyEditors;

namespace Our.Umbraco.Emptiness.Config
{
    public class EmptinessSettings
    {
        public const string SectionName = "Emptiness";

        public string[]? EnabledConverters { get; set; }

        public TrueFalseMode TrueFalseConverter { get; set; }

        public bool IsEnabled<T>() where T : IPropertyValueConverter
        {
            var type = typeof(T);

            if (type == typeof(YesNoDefaultConverter) && TrueFalseConverter == TrueFalseMode.DefaultValue)
            {
                return true;
            }
            else if (type == typeof(NullableYesNoConverter) && TrueFalseConverter == TrueFalseMode.Nullable)
            {
                return true;
            }

            return EnabledConverters?.Contains(typeof(T).Name) == true;
        }

        public static EmptinessSettings DefaultSettings => new()
        {
            EnabledConverters = new[] {
                nameof(NullableDatePickerConverter),
                nameof(NullableDecimalConverter),
                nameof(NullableIntegerConverter),
            },
            TrueFalseConverter = TrueFalseMode.DefaultValue
        };
    }

    public enum TrueFalseMode
    {
        Core,
        DefaultValue,
        Nullable
    }
}
