

namespace Webfuel.Domain.StaticData
{
    public interface IStaticDataService
    {
        Task<IStaticDataModel> GetStaticData();
    }

    [Service(typeof(IStaticDataService))]
    internal class StaticDataService: IStaticDataService
    {
        private readonly IStaticDataCache _staticDataCache;
        
        public StaticDataService(
            IStaticDataCache staticDataCache)
        {
            _staticDataCache = staticDataCache;
        }

        public async Task<IStaticDataModel> GetStaticData()
        {
            return await _staticDataCache.GetStaticData();
        }
    }
}

