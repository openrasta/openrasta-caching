using System.Linq;
using NUnit.Framework;
using OpenRasta.Caching.Pipeline;
using OpenRasta.Testing;

namespace Tests.etag.if_match
{
    public class invalid_combination : contexts.caching
    {
        public invalid_combination()
        {
            given_resource<TestResource>(map => map.Etag(_ => "v1"));
            given_request_header("if-match", Etag.StrongEtag("v1"));
            given_request_header("if-none-match", Etag.StrongEtag("v1"));
            when_executing_request("/TestResource");
        }

        [Test]
        public void warning_header_generated()
        {
            response.Headers["warning"].Split(',')
                .Where(_=>_.Trim().StartsWith("199 If-Lolcat"))
                .Any().ShouldBeTrue();
        }
    }
}