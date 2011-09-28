using System;
using OpenRasta.Caching.Providers;

namespace OpenRasta.Caching.Configuration
{
    public class CachingConfiguration
    {
        public CachingConfiguration()
        {
            CacheProviderType = typeof(InMemoryCacheProvider);
        }
        public bool Automatic { get; set; }

        public Type CacheProviderType { get; set; }
    }
}