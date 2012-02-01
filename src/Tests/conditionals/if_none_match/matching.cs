using NUnit.Framework;
using OpenRasta.Caching.Pipeline;
using OpenRasta.Testing;

namespace Tests.etag.if_none_match
{
    public class matching : contexts.caching
    {
        public matching()
        {
            given_resource<TestResource>(map => map.Etag(_ => "v1"));
            given_request_header("if-none-match", Etag.StrongEtag("v1"));

            when_executing_request("/TestResource");
        }

        [Test]
        public void returns_not_modified()
        {
            response.StatusCode.ShouldBe(304);
        }
    }
}