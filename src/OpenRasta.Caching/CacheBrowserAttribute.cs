using System;

namespace OpenRasta.Caching
{
    public class CacheBrowserAttribute : AbstractCacheAttribute
    {
        public CacheBrowserAttribute()
        {        
        }

        public BrowserCacheLevel Level { get; set; }
    }
    public enum BrowserCacheLevel
    {
        Default,
        None
    }
}