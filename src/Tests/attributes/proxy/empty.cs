using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests.attributes.proxy
{
    public class empty : contexts.attributes
    {
        public empty()
        {
            given_attribute(proxy: new CacheProxyAttribute());
            when_getting_response_caching();
        }

        [Test]
        public void should_be_private()
        {
            cache.CacheDirectives.ShouldBeEmpty();
        }
    }
}