
using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain.StaticData
{
    internal interface IStaticDataCache
    {
        Task<StaticDataModel> GetStaticData();

        void FlushStaticData();
    }

    [Service(typeof(IStaticDataCache))]
    internal class StaticDataCache: IStaticDataCache
    {
        private readonly ITenantCacheService _tenantCacheService;
        private readonly IServiceProvider _serviceProvider;
        
        public StaticDataCache(
            ITenantCacheService tenantCacheService,
            IServiceProvider serviceProvider)
        {
            _tenantCacheService = tenantCacheService;
            _serviceProvider = serviceProvider;
        }

        const string CACHE_KEY = "StaticData";

        public async Task<StaticDataModel> GetStaticData()
        {
            var model = _tenantCacheService.Get<StaticDataModel>(CACHE_KEY);
            if (model != null)
                return model;

            return _tenantCacheService.Set(CACHE_KEY, await StaticDataModel.Load(_serviceProvider));
        }

        public void FlushStaticData()
        {
            _tenantCacheService.Remove(CACHE_KEY);
        }
    }
}

