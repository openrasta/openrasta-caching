using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests.attributes.proxy
{
    public class no_properties : contexts.attributes
    {
        public no_properties()
        {
            given_attribute(new CacheProxyAttribute());
            when_getting_response_caching();
        }

        [Test]
        public void response_not_stored()
        {
            cache.LocalCacheEnabled.ShouldBeFalse();
            cache.LocalCacheMaxAge.ShouldBeNull();
        }

        [Test]
        public void no_response_caching_instruction()
        {
            cache.CacheDirectives.ShouldBeEmpty();
        }
    }
    public class max_age : contexts.attributes
    {
        public max_age()
        {
            given_attribute(new CacheProxyAttribute { MaxAge = "00:01:00"});
            when_getting_response_caching();

        }

        [Test]
        public void local_response_stored()
        {
            cache.LocalCacheEnabled.ShouldBeTrue();
        }

        [Test]
        public void local_max_age_configured()
        {
            cache.LocalCacheMaxAge.ShouldBe(1.Minutes());
        }

        [Test]
        public void response_has_max_age()
        {
            cache.CacheDirectives.ShouldContain("max-age=60");
        }
    }
    public class max_age_no_store : contexts.attributes
    {
        public max_age_no_store()
        {
            given_attribute(new CacheProxyAttribute { MaxAge = "00:01:00", Store = false });
            when_getting_response_caching();
        }

        [Test]
        public void error_is_generated()
        {
            exception.ShouldBeOfType<InvalidOperationException>();
        }

    }
    public class no_store : contexts.attributes
    {
        // For proxies, Store = false maps to private.
        // http://tools.ietf.org/html/draft-ietf-httpbis-p6-cache-16#section-3.2.2
        // The private response directive indicates that the response message
        // is intended for a single user and MUST NOT be stored by a shared
        // cache.  A private cache MAY store the response.
        public no_store()
        {
            given_attribute(new CacheProxyAttribute { Store = false });
            when_getting_response_caching();
        }

        [Test]
        public void local_store_shoudl_be_false()
        {
            cache.LocalCacheEnabled.ShouldBeFalse();

        }

        [Test]
        public void response_not_cached_by_proxies()
        {
            cache.CacheDirectives.ShouldContain("private");
        }
    }
    public class revalidate : contexts.attributes
    {
        //  The proxy-revalidate response directive has the same meaning as
        // the must-revalidate response directive, except that it does not
        // apply to private caches.
        public revalidate()
        {
            given_attribute(new CacheProxyAttribute { MustRevalidate = true });
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

namespace Tests.contexts
{
}