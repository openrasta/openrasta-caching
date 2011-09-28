using System;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Configuration.Fluent.Extensions;
using OpenRasta.Configuration.MetaModel;
using OpenRasta.TypeSystem;

namespace OpenRasta.Caching.Configuration
{
    public class ResourceMapper<T> : IResourceTarget, IResourceMapper<T>
    {
        readonly IResourceTarget _resource;

        public ResourceMapper(IResource resource)
        {
            _resource = (IResourceTarget)resource;
        }

        IMetaModelRepository IFluentTarget.Repository
        {
            get { return _resource.Repository; }
        }

        ITypeSystem IFluentTarget.TypeSystem
        {
            get { return _resource.TypeSystem; }
        }

        ResourceModel IResourceTarget.Resource
        {
            get { return _resource.Resource; }
        }
        public ResourceMapper<T> LastModified(Func<T,DateTimeOffset?> reader)
        {
            Func<object, DateTimeOffset?> untyped = resource => reader((T)resource);

            _resource.Resource.Properties[Keys.LAST_MODIFIED] = untyped;
            return this;
        }
    }
}