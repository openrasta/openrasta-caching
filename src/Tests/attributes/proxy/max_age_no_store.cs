using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;

namespace Tests.attributes.proxy
{
    public class max_age_no_store : contexts.attributes
    {
        public max_age_no_store()
        {
            given_attribute(proxy: new CacheProxyAttribute { MaxAge = "00:01:00", 
                                                             Level = ProxyCacheLevel.None });
            when_getting_response_caching();
        }

        [Test]
        public void error_is_generated()
        {
            exception.ShouldBeOfType<InvalidOperationException>();
        }

    }
}