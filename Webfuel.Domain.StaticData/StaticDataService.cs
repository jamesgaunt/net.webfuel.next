

namespace Webfuel.Domain.StaticData
{
    public partial interface IStaticDataService: IClientConfigurationProvider
    {
        Task<IStaticDataModel> GetStaticData();
    }

    [Service(typeof(IStaticDataService))]
    internal partial class StaticDataService : IStaticDataService
    {
        private readonly IStaticDataCache _staticDataCache;
        
        public StaticDataService(
            IStaticDataCache staticDataCache)
        {
            _staticDataCache = staticDataCache;
        }

        public async Task<IStaticDataModel> GetStaticData()
        {
            var model = await _staticDataCache.GetStaticData();
            return model;
        }

        public Task ProvideClientConfiguration(ClientConfiguration clientConfiguration)
        {
            return Task.CompletedTask;
        }
    }
}

