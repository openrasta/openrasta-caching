using System;
using OpenRasta.Configuration.Fluent;

namespace OpenRasta.Caching.Configuration
{
    public interface IResourceMapper<T> : IResource
    {
        ResourceMapper<T> LastModified(Func<T,DateTimeOffset?> reader);
    }
}