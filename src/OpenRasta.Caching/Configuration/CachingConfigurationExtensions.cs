using OpenRasta.Caching.Pipeline;
using OpenRasta.Configuration.Fluent;

namespace OpenRasta.Caching.Configuration
{
    public static class CachingConfigurationExtensions
    {
        public static CachingBuilder Caching(this IUses uses)
        {
            return new CachingBuilder((IFluentTarget)uses);
        }
        public static IResourceMapper<T> Map<T>(this IResource resource)
        {
            return new ResourceMapper<T>(resource);
        }
    }
}
