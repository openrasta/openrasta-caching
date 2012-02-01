using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified.conditional
{
    public class invalid_value : contexts.caching
    {
        public invalid_value()
        {
            given_current_time(now);
            given_resource<TestResource>(map=>map.LastModified(_=> now - 1.Minutes()));
            given_request_header("if-modified-since", "not-a-date");

            when_executing_request("/TestResource");
        }

        [Test]
        public void conditional_ignored()
        {
            response.StatusCode.ShouldBe(200);
        }
    }
}