using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified
{
    public class valid : contexts.caching
    {
        public valid()
        {
            given_resource(map=>map.LastModified(_=>_.LastModified),
                "/resource", new ResourceWithLastModified { LastModified = now });

            when_executing_request("/resource");
        }
        
        [Test]
        public void request_successful()
        {   
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void last_modified_header_present()
        {
            should_be_date(response.Headers["last-modified"], now);
        }
    }
}