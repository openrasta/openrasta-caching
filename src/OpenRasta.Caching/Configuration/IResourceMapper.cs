using System;
using OpenRasta.Configuration.Fluent;

namespace OpenRasta.Caching.Configuration
{
    public interface IResourceMapper<T> : IResource
    {
        IResourceMapper<T> LastModified(Func<T,DateTimeOffset?> reader);
        IResourceMapper<T> Etag(Func<T, string> reader);
        IResourceMapper<T> Expires(Func<T, TimeSpan> reader);
    }
}