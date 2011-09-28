using NUnit.Framework;
using OpenRasta.Configuration;
using OpenRasta.Testing;

namespace Tests.response_cache_control.handler_attribute
{
    public class not_set : contexts.caching
    {
        public not_set()
        {
            given_has(_ => _.ResourcesOfType<Resource>().AtUri("/").HandledBy<CachingHandler>().AsJsonDataContract().ForMediaType("*/*"));
            when_executing_request("/");
        }

        [Test]
        public void response_is_ok()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void cache_header_not_present()
        {
            response.Headers["cache-control"].ShouldBeNull();
        }
    }
}