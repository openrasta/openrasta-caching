using System;
using OpenRasta.Caching;
using OpenRasta.Caching.Providers;

namespace Tests.contexts
{
    public class attributes
    {
        CacheProxyAttribute _cacheProxyAttrib;
        protected ResponseCachingState cache;
        protected Exception exception;

        protected void given_attribute(CacheProxyAttribute cacheProxyAttribute)
        {
            _cacheProxyAttrib = cacheProxyAttribute;
        }
        protected void when_getting_response_caching()
        {
            try
            {
                cache = CacheResponse.GetResponseDirective(_cacheProxyAttrib);
            }
            catch(Exception e)
            {
                exception = e;
            }
        }
            
    }
}