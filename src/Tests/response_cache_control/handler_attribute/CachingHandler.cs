using System;
using OpenRasta.Caching;
using OpenRasta.Web;

namespace Tests.response_cache_control.handler_attribute
{
    public class CachingHandler
    {
        static Func<Resource> GetProxyCachedMethod = () => new Resource();
        public Resource GetNoCache()
        {
            return new Resource();
        }
        [HttpOperation(ForUriName = "ProxyCached"), CacheProxy(MaxAge = "01:00:00")]
        public Resource GetProxyCached()
        {
            return GetProxyCachedMethod();
        }
    }
}