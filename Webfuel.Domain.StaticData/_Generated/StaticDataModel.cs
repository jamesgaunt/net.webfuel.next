using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain.StaticData
{
    public interface IStaticDataModel
    {
        IReadOnlyList<ApplicationStage> ApplicationStage { get; }
        IReadOnlyList<FundingBody> FundingBody { get; }
        IReadOnlyList<FundingCallType> FundingCallType { get; }
        IReadOnlyList<FundingStream> FundingStream { get; }
        IReadOnlyList<Gender> Gender { get; }
        IReadOnlyList<ProjectStatus> ProjectStatus { get; }
        IReadOnlyList<ResearchMethodology> ResearchMethodology { get; }
        IReadOnlyList<SubmissionStage> SubmissionStage { get; }
        IReadOnlyList<SupportRequestStatus> SupportRequestStatus { get; }
        IReadOnlyList<Title> Title { get; }
        DateTimeOffset LoadedAt { get; }
    }
    internal class StaticDataModel: IStaticDataModel
    {
        public required IReadOnlyList<ApplicationStage> ApplicationStage { get; init; }
        public required IReadOnlyList<FundingBody> FundingBody { get; init; }
        public required IReadOnlyList<FundingCallType> FundingCallType { get; init; }
        public required IReadOnlyList<FundingStream> FundingStream { get; init; }
        public required IReadOnlyList<Gender> Gender { get; init; }
        public required IReadOnlyList<ProjectStatus> ProjectStatus { get; init; }
        public required IReadOnlyList<ResearchMethodology> ResearchMethodology { get; init; }
        public required IReadOnlyList<SubmissionStage> SubmissionStage { get; init; }
        public required IReadOnlyList<SupportRequestStatus> SupportRequestStatus { get; init; }
        public required IReadOnlyList<Title> Title { get; init; }
        public DateTimeOffset LoadedAt { get; init; }
        
        internal static async Task<StaticDataModel> Load(IServiceProvider serviceProvider)
        {
            return new StaticDataModel
            {
                ApplicationStage = await serviceProvider.GetRequiredService<IApplicationStageRepository>().SelectApplicationStage(),
                FundingBody = await serviceProvider.GetRequiredService<IFundingBodyRepository>().SelectFundingBody(),
                FundingCallType = await serviceProvider.GetRequiredService<IFundingCallTypeRepository>().SelectFundingCallType(),
                FundingStream = await serviceProvider.GetRequiredService<IFundingStreamRepository>().SelectFundingStream(),
                Gender = await serviceProvider.GetRequiredService<IGenderRepository>().SelectGender(),
                ProjectStatus = await serviceProvider.GetRequiredService<IProjectStatusRepository>().SelectProjectStatus(),
                ResearchMethodology = await serviceProvider.GetRequiredService<IResearchMethodologyRepository>().SelectResearchMethodology(),
                SubmissionStage = await serviceProvider.GetRequiredService<ISubmissionStageRepository>().SelectSubmissionStage(),
                SupportRequestStatus = await serviceProvider.GetRequiredService<ISupportRequestStatusRepository>().SelectSupportRequestStatus(),
                Title = await serviceProvider.GetRequiredService<ITitleRepository>().SelectTitle(),
                LoadedAt = DateTimeOffset.Now
            };
        }
    }
}

