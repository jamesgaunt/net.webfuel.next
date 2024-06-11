using Microsoft.Extensions.DependencyInjection;

namespace Webfuel.Domain.StaticData
{
    public interface IStaticDataModel
    {
        IReadOnlyList<AgeRange> AgeRange { get; }
        IReadOnlyList<ApplicationStage> ApplicationStage { get; }
        IReadOnlyList<Disability> Disability { get; }
        IReadOnlyList<Ethnicity> Ethnicity { get; }
        IReadOnlyList<FundingBody> FundingBody { get; }
        IReadOnlyList<FundingCallType> FundingCallType { get; }
        IReadOnlyList<FundingStream> FundingStream { get; }
        IReadOnlyList<Gender> Gender { get; }
        IReadOnlyList<HowDidYouFindUs> HowDidYouFindUs { get; }
        IReadOnlyList<IsCTUAlreadyInvolved> IsCTUAlreadyInvolved { get; }
        IReadOnlyList<IsCTUTeamContribution> IsCTUTeamContribution { get; }
        IReadOnlyList<IsFellowship> IsFellowship { get; }
        IReadOnlyList<IsInternationalMultiSiteStudy> IsInternationalMultiSiteStudy { get; }
        IReadOnlyList<IsLeadApplicantNHS> IsLeadApplicantNHS { get; }
        IReadOnlyList<IsPaidRSSAdviserCoapplicant> IsPaidRSSAdviserCoapplicant { get; }
        IReadOnlyList<IsPaidRSSAdviserLead> IsPaidRSSAdviserLead { get; }
        IReadOnlyList<IsPPIEAndEDIContribution> IsPPIEAndEDIContribution { get; }
        IReadOnlyList<IsPrePostAward> IsPrePostAward { get; }
        IReadOnlyList<IsQuantativeTeamContribution> IsQuantativeTeamContribution { get; }
        IReadOnlyList<IsResubmission> IsResubmission { get; }
        IReadOnlyList<IsTeamMembersConsulted> IsTeamMembersConsulted { get; }
        IReadOnlyList<ProfessionalBackground> ProfessionalBackground { get; }
        IReadOnlyList<ProjectStatus> ProjectStatus { get; }
        IReadOnlyList<ReportProvider> ReportProvider { get; }
        IReadOnlyList<ResearcherCareerStage> ResearcherCareerStage { get; }
        IReadOnlyList<ResearcherLocation> ResearcherLocation { get; }
        IReadOnlyList<ResearcherOrganisationType> ResearcherOrganisationType { get; }
        IReadOnlyList<ResearcherRole> ResearcherRole { get; }
        IReadOnlyList<ResearchMethodology> ResearchMethodology { get; }
        IReadOnlyList<RSSHub> RSSHub { get; }
        IReadOnlyList<Site> Site { get; }
        IReadOnlyList<SubmissionOutcome> SubmissionOutcome { get; }
        IReadOnlyList<SubmissionStage> SubmissionStage { get; }
        IReadOnlyList<SupportProvided> SupportProvided { get; }
        IReadOnlyList<SupportRequestStatus> SupportRequestStatus { get; }
        IReadOnlyList<SupportTeam> SupportTeam { get; }
        IReadOnlyList<Title> Title { get; }
        IReadOnlyList<UserDiscipline> UserDiscipline { get; }
        IReadOnlyList<WillStudyUseCTU> WillStudyUseCTU { get; }
        IReadOnlyList<WorkActivity> WorkActivity { get; }
        DateTimeOffset LoadedAt { get; }
    }
    internal class StaticDataModel: IStaticDataModel
    {
        public required IReadOnlyList<AgeRange> AgeRange { get; init; }
        public required IReadOnlyList<ApplicationStage> ApplicationStage { get; init; }
        public required IReadOnlyList<Disability> Disability { get; init; }
        public required IReadOnlyList<Ethnicity> Ethnicity { get; init; }
        public required IReadOnlyList<FundingBody> FundingBody { get; init; }
        public required IReadOnlyList<FundingCallType> FundingCallType { get; init; }
        public required IReadOnlyList<FundingStream> FundingStream { get; init; }
        public required IReadOnlyList<Gender> Gender { get; init; }
        public required IReadOnlyList<HowDidYouFindUs> HowDidYouFindUs { get; init; }
        public required IReadOnlyList<IsCTUAlreadyInvolved> IsCTUAlreadyInvolved { get; init; }
        public required IReadOnlyList<IsCTUTeamContribution> IsCTUTeamContribution { get; init; }
        public required IReadOnlyList<IsFellowship> IsFellowship { get; init; }
        public required IReadOnlyList<IsInternationalMultiSiteStudy> IsInternationalMultiSiteStudy { get; init; }
        public required IReadOnlyList<IsLeadApplicantNHS> IsLeadApplicantNHS { get; init; }
        public required IReadOnlyList<IsPaidRSSAdviserCoapplicant> IsPaidRSSAdviserCoapplicant { get; init; }
        public required IReadOnlyList<IsPaidRSSAdviserLead> IsPaidRSSAdviserLead { get; init; }
        public required IReadOnlyList<IsPPIEAndEDIContribution> IsPPIEAndEDIContribution { get; init; }
        public required IReadOnlyList<IsPrePostAward> IsPrePostAward { get; init; }
        public required IReadOnlyList<IsQuantativeTeamContribution> IsQuantativeTeamContribution { get; init; }
        public required IReadOnlyList<IsResubmission> IsResubmission { get; init; }
        public required IReadOnlyList<IsTeamMembersConsulted> IsTeamMembersConsulted { get; init; }
        public required IReadOnlyList<ProfessionalBackground> ProfessionalBackground { get; init; }
        public required IReadOnlyList<ProjectStatus> ProjectStatus { get; init; }
        public required IReadOnlyList<ReportProvider> ReportProvider { get; init; }
        public required IReadOnlyList<ResearcherCareerStage> ResearcherCareerStage { get; init; }
        public required IReadOnlyList<ResearcherLocation> ResearcherLocation { get; init; }
        public required IReadOnlyList<ResearcherOrganisationType> ResearcherOrganisationType { get; init; }
        public required IReadOnlyList<ResearcherRole> ResearcherRole { get; init; }
        public required IReadOnlyList<ResearchMethodology> ResearchMethodology { get; init; }
        public required IReadOnlyList<RSSHub> RSSHub { get; init; }
        public required IReadOnlyList<Site> Site { get; init; }
        public required IReadOnlyList<SubmissionOutcome> SubmissionOutcome { get; init; }
        public required IReadOnlyList<SubmissionStage> SubmissionStage { get; init; }
        public required IReadOnlyList<SupportProvided> SupportProvided { get; init; }
        public required IReadOnlyList<SupportRequestStatus> SupportRequestStatus { get; init; }
        public required IReadOnlyList<SupportTeam> SupportTeam { get; init; }
        public required IReadOnlyList<Title> Title { get; init; }
        public required IReadOnlyList<UserDiscipline> UserDiscipline { get; init; }
        public required IReadOnlyList<WillStudyUseCTU> WillStudyUseCTU { get; init; }
        public required IReadOnlyList<WorkActivity> WorkActivity { get; init; }
        public DateTimeOffset LoadedAt { get; init; }
        
        internal static async Task<StaticDataModel> Load(IServiceProvider serviceProvider)
        {
            return new StaticDataModel
            {
                AgeRange = await serviceProvider.GetRequiredService<IAgeRangeRepository>().SelectAgeRange(),
                ApplicationStage = await serviceProvider.GetRequiredService<IApplicationStageRepository>().SelectApplicationStage(),
                Disability = await serviceProvider.GetRequiredService<IDisabilityRepository>().SelectDisability(),
                Ethnicity = await serviceProvider.GetRequiredService<IEthnicityRepository>().SelectEthnicity(),
                FundingBody = await serviceProvider.GetRequiredService<IFundingBodyRepository>().SelectFundingBody(),
                FundingCallType = await serviceProvider.GetRequiredService<IFundingCallTypeRepository>().SelectFundingCallType(),
                FundingStream = await serviceProvider.GetRequiredService<IFundingStreamRepository>().SelectFundingStream(),
                Gender = await serviceProvider.GetRequiredService<IGenderRepository>().SelectGender(),
                HowDidYouFindUs = await serviceProvider.GetRequiredService<IHowDidYouFindUsRepository>().SelectHowDidYouFindUs(),
                IsCTUAlreadyInvolved = await serviceProvider.GetRequiredService<IIsCTUAlreadyInvolvedRepository>().SelectIsCTUAlreadyInvolved(),
                IsCTUTeamContribution = await serviceProvider.GetRequiredService<IIsCTUTeamContributionRepository>().SelectIsCTUTeamContribution(),
                IsFellowship = await serviceProvider.GetRequiredService<IIsFellowshipRepository>().SelectIsFellowship(),
                IsInternationalMultiSiteStudy = await serviceProvider.GetRequiredService<IIsInternationalMultiSiteStudyRepository>().SelectIsInternationalMultiSiteStudy(),
                IsLeadApplicantNHS = await serviceProvider.GetRequiredService<IIsLeadApplicantNHSRepository>().SelectIsLeadApplicantNHS(),
                IsPaidRSSAdviserCoapplicant = await serviceProvider.GetRequiredService<IIsPaidRSSAdviserCoapplicantRepository>().SelectIsPaidRSSAdviserCoapplicant(),
                IsPaidRSSAdviserLead = await serviceProvider.GetRequiredService<IIsPaidRSSAdviserLeadRepository>().SelectIsPaidRSSAdviserLead(),
                IsPPIEAndEDIContribution = await serviceProvider.GetRequiredService<IIsPPIEAndEDIContributionRepository>().SelectIsPPIEAndEDIContribution(),
                IsPrePostAward = await serviceProvider.GetRequiredService<IIsPrePostAwardRepository>().SelectIsPrePostAward(),
                IsQuantativeTeamContribution = await serviceProvider.GetRequiredService<IIsQuantativeTeamContributionRepository>().SelectIsQuantativeTeamContribution(),
                IsResubmission = await serviceProvider.GetRequiredService<IIsResubmissionRepository>().SelectIsResubmission(),
                IsTeamMembersConsulted = await serviceProvider.GetRequiredService<IIsTeamMembersConsultedRepository>().SelectIsTeamMembersConsulted(),
                ProfessionalBackground = await serviceProvider.GetRequiredService<IProfessionalBackgroundRepository>().SelectProfessionalBackground(),
                ProjectStatus = await serviceProvider.GetRequiredService<IProjectStatusRepository>().SelectProjectStatus(),
                ReportProvider = await serviceProvider.GetRequiredService<IReportProviderRepository>().SelectReportProvider(),
                ResearcherCareerStage = await serviceProvider.GetRequiredService<IResearcherCareerStageRepository>().SelectResearcherCareerStage(),
                ResearcherLocation = await serviceProvider.GetRequiredService<IResearcherLocationRepository>().SelectResearcherLocation(),
                ResearcherOrganisationType = await serviceProvider.GetRequiredService<IResearcherOrganisationTypeRepository>().SelectResearcherOrganisationType(),
                ResearcherRole = await serviceProvider.GetRequiredService<IResearcherRoleRepository>().SelectResearcherRole(),
                ResearchMethodology = await serviceProvider.GetRequiredService<IResearchMethodologyRepository>().SelectResearchMethodology(),
                RSSHub = await serviceProvider.GetRequiredService<IRSSHubRepository>().SelectRSSHub(),
                Site = await serviceProvider.GetRequiredService<ISiteRepository>().SelectSite(),
                SubmissionOutcome = await serviceProvider.GetRequiredService<ISubmissionOutcomeRepository>().SelectSubmissionOutcome(),
                SubmissionStage = await serviceProvider.GetRequiredService<ISubmissionStageRepository>().SelectSubmissionStage(),
                SupportProvided = await serviceProvider.GetRequiredService<ISupportProvidedRepository>().SelectSupportProvided(),
                SupportRequestStatus = await serviceProvider.GetRequiredService<ISupportRequestStatusRepository>().SelectSupportRequestStatus(),
                SupportTeam = await serviceProvider.GetRequiredService<ISupportTeamRepository>().SelectSupportTeam(),
                Title = await serviceProvider.GetRequiredService<ITitleRepository>().SelectTitle(),
                UserDiscipline = await serviceProvider.GetRequiredService<IUserDisciplineRepository>().SelectUserDiscipline(),
                WillStudyUseCTU = await serviceProvider.GetRequiredService<IWillStudyUseCTURepository>().SelectWillStudyUseCTU(),
                WorkActivity = await serviceProvider.GetRequiredService<IWorkActivityRepository>().SelectWorkActivity(),
                LoadedAt = DateTimeOffset.Now
            };
        }
    }
}

