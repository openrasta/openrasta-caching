using NUnit.Framework;
using OpenRasta.Testing;

namespace Tests.etag
{
    public class valid : contexts.caching
    {
        public valid()
        {
            given_resource<TestResource>(map => map.Etag(_ => "v1"));

            when_executing_request("/TestResource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void etag_present()
        {
            response.ShouldHaveVariantEtag("v1");
        }
    }
}