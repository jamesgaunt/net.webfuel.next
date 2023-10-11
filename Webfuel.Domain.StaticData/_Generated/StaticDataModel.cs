using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain.StaticData
{
    public interface IStaticDataModel
    {
        IReadOnlyList<FundingBody> FundingBody { get; }
        IReadOnlyList<FundingStream> FundingStream { get; }
        IReadOnlyList<Gender> Gender { get; }
        IReadOnlyList<ResearchMethodology> ResearchMethodology { get; }
        IReadOnlyList<Title> Title { get; }
        DateTimeOffset LoadedAt { get; }
    }
    internal class StaticDataModel: IStaticDataModel
    {
        public required IReadOnlyList<FundingBody> FundingBody { get; init; }
        public required IReadOnlyList<FundingStream> FundingStream { get; init; }
        public required IReadOnlyList<Gender> Gender { get; init; }
        public required IReadOnlyList<ResearchMethodology> ResearchMethodology { get; init; }
        public required IReadOnlyList<Title> Title { get; init; }
        public DateTimeOffset LoadedAt { get; init; }
        
        internal static async Task<StaticDataModel> Load(IServiceProvider serviceProvider)
        {
            return new StaticDataModel
            {
                FundingBody = await serviceProvider.GetRequiredService<IFundingBodyRepository>().SelectFundingBody(),
                FundingStream = await serviceProvider.GetRequiredService<IFundingStreamRepository>().SelectFundingStream(),
                Gender = await serviceProvider.GetRequiredService<IGenderRepository>().SelectGender(),
                ResearchMethodology = await serviceProvider.GetRequiredService<IResearchMethodologyRepository>().SelectResearchMethodology(),
                Title = await serviceProvider.GetRequiredService<ITitleRepository>().SelectTitle(),
                LoadedAt = DateTimeOffset.Now
            };
        }
    }
}

