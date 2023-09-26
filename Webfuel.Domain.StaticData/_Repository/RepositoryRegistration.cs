using Microsoft.Extensions.DependencyInjection;
namespace Webfuel.Domain.StaticData
{
    internal static class RepositoryRegistration
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddSingleton<IFundingStreamRepository, FundingStreamRepository>();
            services.AddSingleton<IRepositoryAccessor<FundingStream>, FundingStreamRepositoryAccessor>();
            services.AddSingleton<IRepositoryMapper<FundingStream>, RepositoryDefaultMapper<FundingStream>>();
            
        }
    }
}

