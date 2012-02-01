using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified.conditional
{
    public class unmodified : contexts.caching
    {
        public unmodified()
        {
            given_current_time(now);
             
            given_resource(resource=>resource.Map<ResourceWithLastModified>().LastModified(_=> _.LastModified),
                "/resource", new ResourceWithLastModified { LastModified = now - 1.Minutes() });

            given_request_header("if-modified-since", now);
            when_executing_request("/resource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(304);
        }

        [Test]
        public void last_modified_set()
        {
            should_be_date(response.Headers["last-modified"], now - 1.Minutes());
        }
    }
}