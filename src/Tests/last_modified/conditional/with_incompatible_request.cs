using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Testing;

namespace Tests.last_modified.conditional_request
{
    [TestFixture("If-Match", "ETag")]
    [TestFixture("If-Match", "W/ETag")]
    [TestFixture("If-None-Match", "W/Etag")]
    [TestFixture("If-None-Match", "Etag")]
    [TestFixture("If-Range", "Sat, 29 Oct 1994 19:43:31 GMT", Description = "If-Range HTTP-Date")]
    [TestFixture("If-Range", "W/Etag")]
    [TestFixture("If-Range", "Etag")]
    [TestFixture("Range", "bytes=10-20")]
    public class with_incompatible_request : contexts.caching
    {
        /* A server that evaluates a conditional range request that is
           applicable to one of its representations MUST evaluate the condition
           as false if the entity-tag used as a validator is marked as weak or,
           when an HTTP-date is used as the validator, if the date value is not
           strong in the sense defined by Section 2.2.2 of [Part4].  (A server
           can distinguish between a valid HTTP-date and any form of entity-tag
           by examining the first two characters.)
         * The caching module can make strong guarantees on LastModifiedDate, but
         * doesn't implement range requests so we ignore If-Range and Range.
*        */
        public with_incompatible_request(string header, string value)
        {
            given_time(now);

            given_resource(
                "/resource",
                resource=>resource.Map<ResourceWithLastModified>().LastModified(_=> _.LastModified),
                new ResourceWithLastModified { LastModified = now - 1.Minutes() });

            given_request_header("if-modified-since", now - 2.Minutes());
            given_request_header(header, value);
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
            should_be_date(response.Headers["last-modified"], now - 1.Minutes());
        }
        
    }
}