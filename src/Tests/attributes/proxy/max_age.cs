using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests.attributes.proxy
{
    public class max_age : contexts.attributes
    {
        public max_age()
        {
            given_attribute(proxy: new CacheProxyAttribute { MaxAge = "00:01:00" });
            when_getting_response_caching();
        }

        [Test]
        public void response_has_max_age()
        {
            cache.CacheDirectives.ShouldContain("max-age=60");
        }
    }
}