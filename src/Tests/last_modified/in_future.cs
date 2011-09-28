using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified
{
    public class in_future : contexts.caching
    {
        
        public in_future()
        {
            // 2.2.1
            // An origin server with a clock MUST NOT send a Last-Modified date that
            // is later than the server's time of message origination (Date).
            given_time(now);

            given_resource(
                "/resource",
                resource=>resource.Map<ResourceWithLastModified>().LastModified(_=> now + TimeSpan.FromMinutes(1)),
                new ResourceWithLastModified { LastModified = now });

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