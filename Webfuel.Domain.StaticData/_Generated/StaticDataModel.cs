using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain.StaticData
{
    public interface IStaticDataModel
    {
        IReadOnlyList<Title> Title { get; }
        IReadOnlyList<FundingStream> FundingStream { get; }
        DateTimeOffset LoadedAt { get; }
    }
    internal class StaticDataModel: IStaticDataModel
    {
        public required IReadOnlyList<Title> Title { get; init; }
        public required IReadOnlyList<FundingStream> FundingStream { get; init; }
        public DateTimeOffset LoadedAt { get; init; }
        
        internal static async Task<StaticDataModel> Load(IServiceProvider serviceProvider)
        {
            return new StaticDataModel
            {
                Title = await serviceProvider.GetRequiredService<ITitleRepository>().SelectTitle(),
                FundingStream = await serviceProvider.GetRequiredService<IFundingStreamRepository>().SelectFundingStream(),
                LoadedAt = DateTimeOffset.UtcNow
            };
        }
    }
}

