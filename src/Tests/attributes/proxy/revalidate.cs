using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests.attributes.proxy
{
    public class revalidate : contexts.attributes
    {
        //  The proxy-revalidate response directive has the same meaning as
        // the must-revalidate response directive, except that it does not
        // apply to private caches.
        public revalidate()
        {
            given_attribute(proxy: new CacheProxyAttribute { MustRevalidate = true });
            when_getting_response_caching();
        }

        [Test]
        public void local_store_disabled()
        {
            cache.LocalCacheEnabled.ShouldBeFalse();
        }

        [Test]
        public void response_gets_proxies_to_revalidate()
        {
            cache.CacheDirectives.ShouldContain("proxy-revalidate");

        }
    }
}