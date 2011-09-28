using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified.conditional_request
{
    [SetCulture("en-CA")]
    public class modified : contexts.caching
    {
        DateTimeOffset point_in_time;

        public modified()
        {
            point_in_time = new DateTimeOffset(2011,09,24,22,00,0,0, 2.Hours());

            // last-modified: 24 Sep 2011 23:11:00 +0200 (21:00:00 GMT)
            given_resource(
                "/resource",
                resource=>resource.Map<ResourceWithLastModified>().LastModified(_=> _.LastModified),
                new ResourceWithLastModified { LastModified = point_in_time + 1.Hours() });

            // if-modified-since: 24 Sep 2011 22:00:00 +0200 (20:00:00 GMT)
            given_request_header("if-modified-since", point_in_time.ToUniversalTime().ToString("R"));
            when_executing_request("/resource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void last_modified_set()
        {
            should_be_date(response.Headers["last-modified"], point_in_time + 1.Hours());
        }

        [Test]
        public void no_body_is_set()
        {
            /*
             http://tools.ietf.org/html/draft-ietf-httpbis-p4-conditional-16#section-4.1
             The 304 response MUST NOT contain a message-body, and thus
             is always terminated by the first empty line after the header fields.
            */
            response.Entity.Stream.Length.ShouldBe(0);

        }
    }
}