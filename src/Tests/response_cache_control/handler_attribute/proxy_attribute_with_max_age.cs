using System.Linq;
using NUnit.Framework;
using OpenRasta.Configuration;
using OpenRasta.OperationModel;
using OpenRasta.Testing;

namespace Tests.response_cache_control.handler_attribute
{
    public class proxy_attribute_with_max_age : contexts.caching
    {
        public proxy_attribute_with_max_age()
        {
            given_has(_ => _.ResourcesOfType<Resource>().AtUri("/").Named("ProxyCached").HandledBy<CachingHandler>().AsJsonDataContract().ForMediaType("*/*"));
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
            response.Headers["cache-control"].ShouldBe("max-age=3600");
        }

        [Test]
        public void result_cached_locally()
        {
            SpecExtensions.ShouldHaveCountOf<OutputMember>(cache.Get("/").ShouldHaveCountOf(1)
                                                                    .Single().Value, 1);

        }
    }
}