using System;

namespace Webfuel.Domain.StaticData
{
    public partial interface IStaticDataService
    {
        Task<IReadOnlyList<AgeRange>> SelectAgeRange();
        Task<AgeRange?> GetAgeRange(Guid id);
        Task<AgeRange> RequireAgeRange(Guid id);
        Task<IReadOnlyList<ApplicationStage>> SelectApplicationStage();
        Task<ApplicationStage?> GetApplicationStage(Guid id);
        Task<ApplicationStage> RequireApplicationStage(Guid id);
        Task<IReadOnlyList<Disability>> SelectDisability();
        Task<Disability?> GetDisability(Guid id);
        Task<Disability> RequireDisability(Guid id);
        Task<IReadOnlyList<Ethnicity>> SelectEthnicity();
        Task<Ethnicity?> GetEthnicity(Guid id);
        Task<Ethnicity> RequireEthnicity(Guid id);
        Task<IReadOnlyList<FundingBody>> SelectFundingBody();
        Task<FundingBody?> GetFundingBody(Guid id);
        Task<FundingBody> RequireFundingBody(Guid id);
        Task<IReadOnlyList<FundingCallType>> SelectFundingCallType();
        Task<FundingCallType?> GetFundingCallType(Guid id);
        Task<FundingCallType> RequireFundingCallType(Guid id);
        Task<IReadOnlyList<FundingStream>> SelectFundingStream();
        Task<FundingStream?> GetFundingStream(Guid id);
        Task<FundingStream> RequireFundingStream(Guid id);
        Task<IReadOnlyList<Gender>> SelectGender();
        Task<Gender?> GetGender(Guid id);
        Task<Gender> RequireGender(Guid id);
        Task<IReadOnlyList<HowDidYouFindUs>> SelectHowDidYouFindUs();
        Task<HowDidYouFindUs?> GetHowDidYouFindUs(Guid id);
        Task<HowDidYouFindUs> RequireHowDidYouFindUs(Guid id);
        Task<IReadOnlyList<IsCTUTeamContribution>> SelectIsCTUTeamContribution();
        Task<IsCTUTeamContribution?> GetIsCTUTeamContribution(Guid id);
        Task<IsCTUTeamContribution> RequireIsCTUTeamContribution(Guid id);
        Task<IReadOnlyList<IsFellowship>> SelectIsFellowship();
        Task<IsFellowship?> GetIsFellowship(Guid id);
        Task<IsFellowship> RequireIsFellowship(Guid id);
        Task<IReadOnlyList<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudy();
        Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudy(Guid id);
        Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudy(Guid id);
        Task<IReadOnlyList<IsLeadApplicantNHS>> SelectIsLeadApplicantNHS();
        Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHS(Guid id);
        Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHS(Guid id);
        Task<IReadOnlyList<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContribution();
        Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContribution(Guid id);
        Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContribution(Guid id);
        Task<IReadOnlyList<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContribution();
        Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContribution(Guid id);
        Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContribution(Guid id);
        Task<IReadOnlyList<IsResubmission>> SelectIsResubmission();
        Task<IsResubmission?> GetIsResubmission(Guid id);
        Task<IsResubmission> RequireIsResubmission(Guid id);
        Task<IReadOnlyList<IsTeamMembersConsulted>> SelectIsTeamMembersConsulted();
        Task<IsTeamMembersConsulted?> GetIsTeamMembersConsulted(Guid id);
        Task<IsTeamMembersConsulted> RequireIsTeamMembersConsulted(Guid id);
        Task<IReadOnlyList<ProjectStatus>> SelectProjectStatus();
        Task<ProjectStatus?> GetProjectStatus(Guid id);
        Task<ProjectStatus> RequireProjectStatus(Guid id);
        Task<IReadOnlyList<ResearcherOrganisationType>> SelectResearcherOrganisationType();
        Task<ResearcherOrganisationType?> GetResearcherOrganisationType(Guid id);
        Task<ResearcherOrganisationType> RequireResearcherOrganisationType(Guid id);
        Task<IReadOnlyList<ResearcherRole>> SelectResearcherRole();
        Task<ResearcherRole?> GetResearcherRole(Guid id);
        Task<ResearcherRole> RequireResearcherRole(Guid id);
        Task<IReadOnlyList<ResearchMethodology>> SelectResearchMethodology();
        Task<ResearchMethodology?> GetResearchMethodology(Guid id);
        Task<ResearchMethodology> RequireResearchMethodology(Guid id);
        Task<IReadOnlyList<Site>> SelectSite();
        Task<Site?> GetSite(Guid id);
        Task<Site> RequireSite(Guid id);
        Task<IReadOnlyList<SubmissionOutcome>> SelectSubmissionOutcome();
        Task<SubmissionOutcome?> GetSubmissionOutcome(Guid id);
        Task<SubmissionOutcome> RequireSubmissionOutcome(Guid id);
        Task<IReadOnlyList<SubmissionStage>> SelectSubmissionStage();
        Task<SubmissionStage?> GetSubmissionStage(Guid id);
        Task<SubmissionStage> RequireSubmissionStage(Guid id);
        Task<IReadOnlyList<SupportProvided>> SelectSupportProvided();
        Task<SupportProvided?> GetSupportProvided(Guid id);
        Task<SupportProvided> RequireSupportProvided(Guid id);
        Task<IReadOnlyList<SupportRequestStatus>> SelectSupportRequestStatus();
        Task<SupportRequestStatus?> GetSupportRequestStatus(Guid id);
        Task<SupportRequestStatus> RequireSupportRequestStatus(Guid id);
        Task<IReadOnlyList<SupportTeam>> SelectSupportTeam();
        Task<SupportTeam?> GetSupportTeam(Guid id);
        Task<SupportTeam> RequireSupportTeam(Guid id);
        Task<IReadOnlyList<Title>> SelectTitle();
        Task<Title?> GetTitle(Guid id);
        Task<Title> RequireTitle(Guid id);
        Task<IReadOnlyList<UserDiscipline>> SelectUserDiscipline();
        Task<UserDiscipline?> GetUserDiscipline(Guid id);
        Task<UserDiscipline> RequireUserDiscipline(Guid id);
        Task<IReadOnlyList<WorkActivity>> SelectWorkActivity();
        Task<WorkActivity?> GetWorkActivity(Guid id);
        Task<WorkActivity> RequireWorkActivity(Guid id);
    }
    internal partial class StaticDataService: IStaticDataService
    {
        public async Task<IReadOnlyList<AgeRange>> SelectAgeRange()
        {
            return (await GetStaticData()).AgeRange;
        }
        
        public async Task<AgeRange?> GetAgeRange(Guid id)
        {
            return (await GetStaticData()).AgeRange.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<AgeRange> RequireAgeRange(Guid id)
        {
            return (await GetStaticData()).AgeRange.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<ApplicationStage>> SelectApplicationStage()
        {
            return (await GetStaticData()).ApplicationStage;
        }
        
        public async Task<ApplicationStage?> GetApplicationStage(Guid id)
        {
            return (await GetStaticData()).ApplicationStage.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<ApplicationStage> RequireApplicationStage(Guid id)
        {
            return (await GetStaticData()).ApplicationStage.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Disability>> SelectDisability()
        {
            return (await GetStaticData()).Disability;
        }
        
        public async Task<Disability?> GetDisability(Guid id)
        {
            return (await GetStaticData()).Disability.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<Disability> RequireDisability(Guid id)
        {
            return (await GetStaticData()).Disability.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Ethnicity>> SelectEthnicity()
        {
            return (await GetStaticData()).Ethnicity;
        }
        
        public async Task<Ethnicity?> GetEthnicity(Guid id)
        {
            return (await GetStaticData()).Ethnicity.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<Ethnicity> RequireEthnicity(Guid id)
        {
            return (await GetStaticData()).Ethnicity.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<FundingBody>> SelectFundingBody()
        {
            return (await GetStaticData()).FundingBody;
        }
        
        public async Task<FundingBody?> GetFundingBody(Guid id)
        {
            return (await GetStaticData()).FundingBody.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<FundingBody> RequireFundingBody(Guid id)
        {
            return (await GetStaticData()).FundingBody.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<FundingCallType>> SelectFundingCallType()
        {
            return (await GetStaticData()).FundingCallType;
        }
        
        public async Task<FundingCallType?> GetFundingCallType(Guid id)
        {
            return (await GetStaticData()).FundingCallType.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<FundingCallType> RequireFundingCallType(Guid id)
        {
            return (await GetStaticData()).FundingCallType.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<FundingStream>> SelectFundingStream()
        {
            return (await GetStaticData()).FundingStream;
        }
        
        public async Task<FundingStream?> GetFundingStream(Guid id)
        {
            return (await GetStaticData()).FundingStream.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<FundingStream> RequireFundingStream(Guid id)
        {
            return (await GetStaticData()).FundingStream.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Gender>> SelectGender()
        {
            return (await GetStaticData()).Gender;
        }
        
        public async Task<Gender?> GetGender(Guid id)
        {
            return (await GetStaticData()).Gender.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<Gender> RequireGender(Guid id)
        {
            return (await GetStaticData()).Gender.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<HowDidYouFindUs>> SelectHowDidYouFindUs()
        {
            return (await GetStaticData()).HowDidYouFindUs;
        }
        
        public async Task<HowDidYouFindUs?> GetHowDidYouFindUs(Guid id)
        {
            return (await GetStaticData()).HowDidYouFindUs.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<HowDidYouFindUs> RequireHowDidYouFindUs(Guid id)
        {
            return (await GetStaticData()).HowDidYouFindUs.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsCTUTeamContribution>> SelectIsCTUTeamContribution()
        {
            return (await GetStaticData()).IsCTUTeamContribution;
        }
        
        public async Task<IsCTUTeamContribution?> GetIsCTUTeamContribution(Guid id)
        {
            return (await GetStaticData()).IsCTUTeamContribution.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsCTUTeamContribution> RequireIsCTUTeamContribution(Guid id)
        {
            return (await GetStaticData()).IsCTUTeamContribution.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsFellowship>> SelectIsFellowship()
        {
            return (await GetStaticData()).IsFellowship;
        }
        
        public async Task<IsFellowship?> GetIsFellowship(Guid id)
        {
            return (await GetStaticData()).IsFellowship.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsFellowship> RequireIsFellowship(Guid id)
        {
            return (await GetStaticData()).IsFellowship.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsInternationalMultiSiteStudy>> SelectIsInternationalMultiSiteStudy()
        {
            return (await GetStaticData()).IsInternationalMultiSiteStudy;
        }
        
        public async Task<IsInternationalMultiSiteStudy?> GetIsInternationalMultiSiteStudy(Guid id)
        {
            return (await GetStaticData()).IsInternationalMultiSiteStudy.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsInternationalMultiSiteStudy> RequireIsInternationalMultiSiteStudy(Guid id)
        {
            return (await GetStaticData()).IsInternationalMultiSiteStudy.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsLeadApplicantNHS>> SelectIsLeadApplicantNHS()
        {
            return (await GetStaticData()).IsLeadApplicantNHS;
        }
        
        public async Task<IsLeadApplicantNHS?> GetIsLeadApplicantNHS(Guid id)
        {
            return (await GetStaticData()).IsLeadApplicantNHS.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsLeadApplicantNHS> RequireIsLeadApplicantNHS(Guid id)
        {
            return (await GetStaticData()).IsLeadApplicantNHS.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsPPIEAndEDIContribution>> SelectIsPPIEAndEDIContribution()
        {
            return (await GetStaticData()).IsPPIEAndEDIContribution;
        }
        
        public async Task<IsPPIEAndEDIContribution?> GetIsPPIEAndEDIContribution(Guid id)
        {
            return (await GetStaticData()).IsPPIEAndEDIContribution.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsPPIEAndEDIContribution> RequireIsPPIEAndEDIContribution(Guid id)
        {
            return (await GetStaticData()).IsPPIEAndEDIContribution.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsQuantativeTeamContribution>> SelectIsQuantativeTeamContribution()
        {
            return (await GetStaticData()).IsQuantativeTeamContribution;
        }
        
        public async Task<IsQuantativeTeamContribution?> GetIsQuantativeTeamContribution(Guid id)
        {
            return (await GetStaticData()).IsQuantativeTeamContribution.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsQuantativeTeamContribution> RequireIsQuantativeTeamContribution(Guid id)
        {
            return (await GetStaticData()).IsQuantativeTeamContribution.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsResubmission>> SelectIsResubmission()
        {
            return (await GetStaticData()).IsResubmission;
        }
        
        public async Task<IsResubmission?> GetIsResubmission(Guid id)
        {
            return (await GetStaticData()).IsResubmission.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsResubmission> RequireIsResubmission(Guid id)
        {
            return (await GetStaticData()).IsResubmission.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<IsTeamMembersConsulted>> SelectIsTeamMembersConsulted()
        {
            return (await GetStaticData()).IsTeamMembersConsulted;
        }
        
        public async Task<IsTeamMembersConsulted?> GetIsTeamMembersConsulted(Guid id)
        {
            return (await GetStaticData()).IsTeamMembersConsulted.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<IsTeamMembersConsulted> RequireIsTeamMembersConsulted(Guid id)
        {
            return (await GetStaticData()).IsTeamMembersConsulted.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<ProjectStatus>> SelectProjectStatus()
        {
            return (await GetStaticData()).ProjectStatus;
        }
        
        public async Task<ProjectStatus?> GetProjectStatus(Guid id)
        {
            return (await GetStaticData()).ProjectStatus.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<ProjectStatus> RequireProjectStatus(Guid id)
        {
            return (await GetStaticData()).ProjectStatus.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<ResearcherOrganisationType>> SelectResearcherOrganisationType()
        {
            return (await GetStaticData()).ResearcherOrganisationType;
        }
        
        public async Task<ResearcherOrganisationType?> GetResearcherOrganisationType(Guid id)
        {
            return (await GetStaticData()).ResearcherOrganisationType.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<ResearcherOrganisationType> RequireResearcherOrganisationType(Guid id)
        {
            return (await GetStaticData()).ResearcherOrganisationType.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<ResearcherRole>> SelectResearcherRole()
        {
            return (await GetStaticData()).ResearcherRole;
        }
        
        public async Task<ResearcherRole?> GetResearcherRole(Guid id)
        {
            return (await GetStaticData()).ResearcherRole.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<ResearcherRole> RequireResearcherRole(Guid id)
        {
            return (await GetStaticData()).ResearcherRole.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<ResearchMethodology>> SelectResearchMethodology()
        {
            return (await GetStaticData()).ResearchMethodology;
        }
        
        public async Task<ResearchMethodology?> GetResearchMethodology(Guid id)
        {
            return (await GetStaticData()).ResearchMethodology.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<ResearchMethodology> RequireResearchMethodology(Guid id)
        {
            return (await GetStaticData()).ResearchMethodology.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Site>> SelectSite()
        {
            return (await GetStaticData()).Site;
        }
        
        public async Task<Site?> GetSite(Guid id)
        {
            return (await GetStaticData()).Site.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<Site> RequireSite(Guid id)
        {
            return (await GetStaticData()).Site.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<SubmissionOutcome>> SelectSubmissionOutcome()
        {
            return (await GetStaticData()).SubmissionOutcome;
        }
        
        public async Task<SubmissionOutcome?> GetSubmissionOutcome(Guid id)
        {
            return (await GetStaticData()).SubmissionOutcome.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<SubmissionOutcome> RequireSubmissionOutcome(Guid id)
        {
            return (await GetStaticData()).SubmissionOutcome.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<SubmissionStage>> SelectSubmissionStage()
        {
            return (await GetStaticData()).SubmissionStage;
        }
        
        public async Task<SubmissionStage?> GetSubmissionStage(Guid id)
        {
            return (await GetStaticData()).SubmissionStage.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<SubmissionStage> RequireSubmissionStage(Guid id)
        {
            return (await GetStaticData()).SubmissionStage.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<SupportProvided>> SelectSupportProvided()
        {
            return (await GetStaticData()).SupportProvided;
        }
        
        public async Task<SupportProvided?> GetSupportProvided(Guid id)
        {
            return (await GetStaticData()).SupportProvided.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<SupportProvided> RequireSupportProvided(Guid id)
        {
            return (await GetStaticData()).SupportProvided.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<SupportRequestStatus>> SelectSupportRequestStatus()
        {
            return (await GetStaticData()).SupportRequestStatus;
        }
        
        public async Task<SupportRequestStatus?> GetSupportRequestStatus(Guid id)
        {
            return (await GetStaticData()).SupportRequestStatus.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<SupportRequestStatus> RequireSupportRequestStatus(Guid id)
        {
            return (await GetStaticData()).SupportRequestStatus.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<SupportTeam>> SelectSupportTeam()
        {
            return (await GetStaticData()).SupportTeam;
        }
        
        public async Task<SupportTeam?> GetSupportTeam(Guid id)
        {
            return (await GetStaticData()).SupportTeam.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<SupportTeam> RequireSupportTeam(Guid id)
        {
            return (await GetStaticData()).SupportTeam.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<Title>> SelectTitle()
        {
            return (await GetStaticData()).Title;
        }
        
        public async Task<Title?> GetTitle(Guid id)
        {
            return (await GetStaticData()).Title.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<Title> RequireTitle(Guid id)
        {
            return (await GetStaticData()).Title.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<UserDiscipline>> SelectUserDiscipline()
        {
            return (await GetStaticData()).UserDiscipline;
        }
        
        public async Task<UserDiscipline?> GetUserDiscipline(Guid id)
        {
            return (await GetStaticData()).UserDiscipline.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<UserDiscipline> RequireUserDiscipline(Guid id)
        {
            return (await GetStaticData()).UserDiscipline.First(p => p.Id == id);
        }
        
        public async Task<IReadOnlyList<WorkActivity>> SelectWorkActivity()
        {
            return (await GetStaticData()).WorkActivity;
        }
        
        public async Task<WorkActivity?> GetWorkActivity(Guid id)
        {
            return (await GetStaticData()).WorkActivity.FirstOrDefault(p => p.Id == id);
        }
        
        public async Task<WorkActivity> RequireWorkActivity(Guid id)
        {
            return (await GetStaticData()).WorkActivity.First(p => p.Id == id);
        }
        
    }
}

