using Umbraco.Cms.Core.DependencyInjection;

namespace Our.Umbraco.Emptiness.Config
{
    public static class IUmbracoBuilderExtensions
    {
        public static EmptinessCollectionBuilder PvcCollectionBuilder(this IUmbracoBuilder builder, EmptinessSettings settings)
        {
            return new EmptinessCollectionBuilder(builder.PropertyValueConverters(), settings);
        }
    }
}
