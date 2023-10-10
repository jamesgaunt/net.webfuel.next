

namespace Webfuel.Domain.StaticData
{
    public partial interface IStaticDataService: IClientConfigurationProvider
    {
        Task<IStaticDataModel> GetStaticData();
    }

    [Service(typeof(IStaticDataService), typeof(IClientConfigurationProvider))]
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
            clientConfiguration.StaticDataMenu.AddChild(name: "Title", action: "/static-data/title");
            clientConfiguration.StaticDataMenu.AddChild(name: "FundingStream", action: "/static-data/funding-stream");
            return Task.CompletedTask;
        }
    }
}

