﻿using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Caching.Pipeline;
using OpenRasta.Configuration;
using OpenRasta.Pipeline;
using OpenRasta.Testing;
using OpenRasta.Web;
using Tests.last_modified.conditional;

namespace Tests.last_modified
{
    public class already_set : contexts.caching
    {
     
        public already_set()
        {
            // 2.2.1
            // An origin server with a clock MUST NOT send a Last-Modified date that
            // is later than the server's time of message origination (Date).
            given_time(now);
            given_uses(_ => _.PipelineContributor<LastModifiedInPast>());
            given_resource<TestResource>(map => map.LastModified(_=>now));

            when_executing_request("/TestResource");
        }

        [Test]
        public void request_successful()
        {
            response.StatusCode.ShouldBe(200);
        }

        [Test]
        public void last_modified_header_not_overridden()
        {
            DateTimeOffset.Parse(response.Headers["last-modified"])
                .ShouldNotBe(now.Value);
        }
        class LastModifiedInPast : HeaderSetter
        {
            public LastModifiedInPast() : base("last-modified", (ServerClock.UtcNow() - TimeSpan.FromSeconds(10)).ToString("R")) { }
        }
    }
}