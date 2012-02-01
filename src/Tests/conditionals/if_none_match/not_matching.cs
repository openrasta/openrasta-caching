using NUnit.Framework;
using OpenRasta.Caching.Pipeline;
using OpenRasta.Testing;

namespace Tests.etag.if_none_match
{
    public class not_matching : contexts.caching
    {
        public not_matching()
        {
            given_resource<TestResource>(map => map.Etag(_ => "v2"));
            given_request_header("if-none-match", Etag.StrongEtag("v1"));

            when_executing_request("/TestResource");
        }

        [Test]
        public void returns_precondition_failed()
        {
            response.StatusCode.ShouldBe(200);
        }
    }
}