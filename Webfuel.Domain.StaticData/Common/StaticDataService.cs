

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
            clientConfiguration.StaticDataMenu.AddChild(name: "Funding Stream", action: "/static-data/funding-stream");
            clientConfiguration.StaticDataMenu.AddChild(name: "Funding Body", action: "/static-data/funding-body");
            clientConfiguration.StaticDataMenu.AddChild(name: "Gender", action: "/static-data/gender");
            clientConfiguration.StaticDataMenu.AddChild(name: "Research Methodology", action: "/static-data/research-methodology");

            return Task.CompletedTask;

        }
    }
}

