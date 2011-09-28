using System;
using NUnit.Framework;
using OpenRasta.Caching;
using OpenRasta.Caching.Configuration;
using OpenRasta.Caching.Pipeline;
using OpenRasta.Configuration;
using OpenRasta.Pipeline;
using OpenRasta.Testing;
using OpenRasta.Web;

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
            given_uses(_ => _.PipelineContributor<LastModifierSetter>());
            given_resource(
                "/resource",
                resource=>resource.Map<ResourceWithLastModified>().LastModified(_=> _.LastModified),
                new ResourceWithLastModified { LastModified = now });
            
            when_executing_request("/resource");
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
        class LastModifierSetter : IPipelineContributor
        {
            public void Initialize(IPipeline pipelineRunner)
            {
                pipelineRunner.Notify(WriteLastModified)
                    .After<KnownStages.IOperationExecution>().And.Before<LastModifiedContributor>();
            }

            PipelineContinuation WriteLastModified(ICommunicationContext arg)
            {
                arg.Response.Headers["last-modified"] = (ServerClock.UtcNow() - TimeSpan.FromSeconds(10)).ToString("R");
                return PipelineContinuation.Continue;
            }
        }
    }
}