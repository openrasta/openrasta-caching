using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Testing;
using Tests.last_modified;

namespace Tests.conditionals.if_modified_since
{
    public class modified : contexts.caching
    {
        public modified()
        {
            given_current_time(now);
            given_resource<TestResource>(map => map.LastModified(_ => now - 1.Minutes()));
            given_request_header("if-modified-since", now - 2.Minutes());

            when_executing_request("/TestResource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void last_modified_set()
        {
            should_be_date(response.Headers["last-modified"], now - 1.Minutes());
        }
    }
}