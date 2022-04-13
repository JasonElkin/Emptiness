using Microsoft.Extensions.Configuration;
using Our.Umbraco.Emptiness.Config;
using Our.Umbraco.Emptiness.PropertyValueConverters;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.Emptiness
{
    public class Composer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            var settings = builder.Config.GetSection(EmptinessSettings.SectionName)
                .Get<EmptinessSettings>() ?? EmptinessSettings.DefaultSettings;

            builder.PvcCollectionBuilder(settings)
                .InsertIfEnabled<NullableDatePickerConverter>()
                .InsertIfEnabled<NullableDecimalConverter>()
                .InsertIfEnabled<NullableIntegerConverter>()
                .InsertIfEnabled<NullableLabelConverter>()
                .InsertIfEnabled<YesNoDefaultConverter>()
                .InsertIfEnabled<NullableYesNoConverter>();
        }
    }
}
