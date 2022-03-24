using Our.Umbraco.Emptiness.PropertyValueConverters;
using System.Linq;

namespace Our.Umbraco.Emptiness.Config
{
    public class EmptinessSettings
    {
        public const string SectionName = "Emptiness";

        public string[] EnabledConverters { get; set; } = new[] {
            nameof(NullableDatePickerConverter),
            nameof(NullableDecimalConverter),
            nameof(NullableIntegerConverter),
            nameof(NullableLabelConverter),
        };

        public TrueFalseMode TrueFalseConverter { get; set; } = TrueFalseMode.DefaultValue;

        public bool IsEnabled<T>() where T : IEmptinessPropertyValueConverter
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

            return EnabledConverters.Contains(typeof(T).Name);
        }
    }

    public enum TrueFalseMode
    {
        Core,
        DefaultValue,
        Nullable
    }
}
