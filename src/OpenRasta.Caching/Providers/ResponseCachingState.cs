using System;
using System.Collections.Generic;
using OpenRasta.OperationModel;

namespace OpenRasta.Caching.Providers
{
    public class ResponseCachingState
    {
        public ResponseCachingState()
        {
            CacheDirectives = new List<string>();
        }
        public TimeSpan? LocalCacheMaxAge { get; set; }
        public bool LocalCacheEnabled { get; set; }

        public ICollection<string> CacheDirectives { get; set; }

        public IEnumerable<OutputMember> LocalResult { get; set; }
    }
}