using NUnit.Framework;
using OpenRasta.Testing;

namespace Tests.last_modified
{
    public class not_mapped : contexts.caching
    {
        public not_mapped()
        {
            given_time(now);
            given_resource(
                "/resource",
                new ResourceWithLastModified { LastModified = now });

            when_executing_request("/resource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(200);

        }
        [Test]
        public void header_not_present()
        {
            response.Headers.ContainsKey("last-modified").ShouldBeFalse();
        }
    }
}