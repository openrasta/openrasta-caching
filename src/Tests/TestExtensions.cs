using OpenRasta.Caching.Pipeline;
using OpenRasta.Testing;
using OpenRasta.Web;

namespace Tests
{
    public static class TestExtensions
    {
        public static IResponse ShouldHaveVariantEtag(this IResponse response, string etag)
        {
            response.Headers.ContainsKey(HttpHeaders.ETAG).ShouldBeTrue();
            response.Headers[HttpHeaders.ETAG].EndsWith(":" + etag + '"').ShouldBeTrue();
            return response;
        }
    }
}