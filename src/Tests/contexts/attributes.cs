using System;
using OpenRasta.Caching;
using OpenRasta.Caching.Providers;

namespace Tests.contexts
{
    public class attributes
    {
        CacheProxyAttribute _proxy;
        protected ResponseCachingState cache;
        protected Exception exception;
        CacheServerAttribute _server;
        CacheBrowserAttribute _browser;

        protected void given_attribute(
            CacheBrowserAttribute browser = null,
            CacheProxyAttribute proxy = null,
            CacheServerAttribute server = null)
        {
            _browser = browser;
            _proxy = proxy;
            _server = server;
        }
        protected void when_getting_response_caching()
        {
            try
            {
                cache = CacheResponse.GetResponseDirective(_proxy, _browser, _server);
            }
            catch (Exception e)
            {
                exception = e;
            }
        }
            
    }
}