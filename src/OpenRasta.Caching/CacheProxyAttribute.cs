using System;

namespace OpenRasta.Caching
{
    public class CacheProxyAttribute : AbstractCacheAttribute
    {
        public CacheProxyAttribute()
        {
        }

        public static readonly CacheProxyAttribute Default = new CacheProxyAttribute();
    }
}