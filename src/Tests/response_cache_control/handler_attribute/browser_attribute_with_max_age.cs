using System.Linq;
using NUnit.Framework;
using OpenRasta.Configuration;
using OpenRasta.OperationModel;
using OpenRasta.Testing;

namespace Tests.response_cache_control.handler_attribute
{
    public class browser_attribute_with_max_age : contexts.caching
    {
        public browser_attribute_with_max_age()
        {
            given_has(_ => _.ResourcesOfType<Resource>()
                            .AtUri("/").Named("CacheBrowser")
                            .HandledBy<CachingHandler>()
                            .AsJsonDataContract().ForMediaType("*/*"));
            when_executing_request("/");
        }

        [Test]
        public void response_is_ok()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void cache_header_present()
        {
            response.Headers["cache-control"].ShouldBe("private, max-age=3600");
        }
    }
}