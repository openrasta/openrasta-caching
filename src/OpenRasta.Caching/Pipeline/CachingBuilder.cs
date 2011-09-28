using OpenRasta.Caching.Configuration;
using OpenRasta.Caching.Providers;
using OpenRasta.Configuration.Fluent;
using OpenRasta.Configuration.MetaModel;
using OpenRasta.Configuration.MetaModel.Handlers;
using OpenRasta.DI;
using OpenRasta.OperationModel.Interceptors;
using OpenRasta.Pipeline;

namespace OpenRasta.Caching.Pipeline
{
    public class CachingBuilder
    {
        CachingConfiguration _conf = new CachingConfiguration();

        public CachingBuilder(IFluentTarget uses)
        {
            // TODO: Dragons dragons, if no resolver registered we should add a custom registration
            // so we can support both 2.1 and 2.0
            DependencyManager.GetService<IDependencyResolver>().AddDependency<IMetaModelHandler, CacheConfigurationHandler>(DependencyLifetime.Transient);

            uses.Repository.CustomRegistrations.Add(new DependencyRegistrationModel(
                                                        typeof(IOperationInterceptor),
                                                        typeof(CachingInterceptor),
                                                        DependencyLifetime.Transient));
            uses.Repository.CustomRegistrations.Add(new DependencyRegistrationModel(
                                                        typeof(IPipelineContributor),
                                                        typeof(CachingContributor),
                                                        DependencyLifetime.Transient));
            uses.Repository.CustomRegistrations.Add(new DependencyRegistrationModel(
                                                        typeof(IPipelineContributor),
                                                        typeof(LastModifiedContributor),
                                                        DependencyLifetime.Transient));
            uses.Repository.CustomRegistrations.Add(_conf);
        }
        public CachingBuilder Auto()
        {
            _conf.Automatic = true;
            return this;
        }
        public CachingBuilder CacheProvider<T>() where T: ICacheProvider
        {
            _conf.CacheProviderType = typeof(T);
            return this;
        }

    }
}