using Umbraco.Cms.Core.PropertyEditors;

namespace Our.Umbraco.Emptiness.Config
{
    public class EmptinessCollectionBuilder : PropertyValueConverterCollectionBuilder
    {
        private readonly PropertyValueConverterCollectionBuilder builder;
        private readonly EmptinessSettings settings;

        public EmptinessCollectionBuilder(PropertyValueConverterCollectionBuilder builder, EmptinessSettings settings)
        {
            this.builder = builder;
            this.settings = settings;
        }

        public EmptinessCollectionBuilder InsertIfEnabled<T>() where T : IEmptinessPropertyValueConverter
        {
            if (settings.IsEnabled<T>())
            {
                builder.Insert<T>();
            }
            return this;
        }
    }
}
