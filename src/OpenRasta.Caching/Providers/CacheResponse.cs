using System;

namespace OpenRasta.Caching.Providers
{
    public static class CacheResponse
    {
        public static ResponseCachingState GetResponseDirective(CacheProxyAttribute cacheProxy)
        {
            var response = new ResponseCachingState();
            cacheProxy = cacheProxy ?? CacheProxyAttribute.Default;
            TimeSpan parsedValue;
            if (cacheProxy.MaxAge != null && cacheProxy.Store == false)
                throw new InvalidOperationException("Cannot set MaxAge to a value and Store=false.");
            if (cacheProxy.MaxAge != null && TimeSpan.TryParse(cacheProxy.MaxAge, out parsedValue))
            {
                response.LocalCacheEnabled = true;
                response.LocalCacheMaxAge = parsedValue;
                response.CacheDirectives.Add("max-age=" + parsedValue.TotalSeconds);
            }
            if (cacheProxy.Store == false)
                response.CacheDirectives.Add("private");
            if (cacheProxy.MustRevalidate)
                response.CacheDirectives.Add("proxy-revalidate");
            return response;
        }   
    }
}