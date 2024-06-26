import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { DashboardApi } from './dashboard.api';
import { EmailTemplateApi } from './email-template.api';
import { FileStorageEntryApi } from './file-storage-entry.api';
import { HeartbeatApi } from './heartbeat.api';
import { PingApi } from './ping.api';
import { StaticDataApi } from './static-data.api';
import { ProjectAdviserApi } from './project-adviser.api';
import { ProjectApi } from './project.api';
import { ProjectChangeLogApi } from './project-change-log.api';
import { ProjectSubmissionApi } from './project-submission.api';
import { ProjectSupportApi } from './project-support.api';
import { ReportApi } from './report.api';
import { ReportDesignApi } from './report-design.api';
import { ReportGeneratorApi } from './report-generator.api';
import { ReportGroupApi } from './report-group.api';
import { SupportRequestApi } from './support-request.api';
import { SupportRequestChangeLogApi } from './support-request-change-log.api';
import { SupportTeamUserApi } from './support-team-user.api';
import { UserActivityApi } from './user-activity.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { UserLoginApi } from './user-login.api';
import { AgeRangeApi } from './age-range.api';
import { ApplicationStageApi } from './application-stage.api';
import { DisabilityApi } from './disability.api';
import { EthnicityApi } from './ethnicity.api';
import { FundingBodyApi } from './funding-body.api';
import { FundingCallTypeApi } from './funding-call-type.api';
import { FundingStreamApi } from './funding-stream.api';
import { GenderApi } from './gender.api';
import { HowDidYouFindUsApi } from './how-did-you-find-us.api';
import { IsCTUAlreadyInvolvedApi } from './is-ctualready-involved.api';
import { IsCTUTeamContributionApi } from './is-ctuteam-contribution.api';
import { IsFellowshipApi } from './is-fellowship.api';
import { IsInternationalMultiSiteStudyApi } from './is-international-multi-site-study.api';
import { IsLeadApplicantNHSApi } from './is-lead-applicant-nhs.api';
import { IsPaidRSSAdviserCoapplicantApi } from './is-paid-rssadviser-coapplicant.api';
import { IsPaidRSSAdviserLeadApi } from './is-paid-rssadviser-lead.api';
import { IsPPIEAndEDIContributionApi } from './is-ppieand-edicontribution.api';
import { IsPrePostAwardApi } from './is-pre-post-award.api';
import { IsQuantativeTeamContributionApi } from './is-quantative-team-contribution.api';
import { IsResubmissionApi } from './is-resubmission.api';
import { IsTeamMembersConsultedApi } from './is-team-members-consulted.api';
import { ProfessionalBackgroundApi } from './professional-background.api';
import { ProjectStatusApi } from './project-status.api';
import { ReportProviderApi } from './report-provider.api';
import { ResearcherCareerStageApi } from './researcher-career-stage.api';
import { ResearcherLocationApi } from './researcher-location.api';
import { ResearcherOrganisationTypeApi } from './researcher-organisation-type.api';
import { ResearcherRoleApi } from './researcher-role.api';
import { ResearchMethodologyApi } from './research-methodology.api';
import { RSSHubApi } from './rsshub.api';
import { SiteApi } from './site.api';
import { SubmissionOutcomeApi } from './submission-outcome.api';
import { SubmissionStageApi } from './submission-stage.api';
import { SupportProvidedApi } from './support-provided.api';
import { SupportRequestStatusApi } from './support-request-status.api';
import { SupportTeamApi } from './support-team.api';
import { TitleApi } from './title.api';
import { UserDisciplineApi } from './user-discipline.api';
import { WillStudyUseCTUApi } from './will-study-use-ctu.api';
import { WorkActivityApi } from './work-activity.api';
import { StaticDataCache } from './static-data.cache';

@NgModule({
    providers: [
        ConfigurationApi,
        DashboardApi,
        EmailTemplateApi,
        FileStorageEntryApi,
        HeartbeatApi,
        PingApi,
        StaticDataApi,
        ProjectAdviserApi,
        ProjectApi,
        ProjectChangeLogApi,
        ProjectSubmissionApi,
        ProjectSupportApi,
        ReportApi,
        ReportDesignApi,
        ReportGeneratorApi,
        ReportGroupApi,
        SupportRequestApi,
        SupportRequestChangeLogApi,
        SupportTeamUserApi,
        UserActivityApi,
        UserApi,
        UserGroupApi,
        UserLoginApi,
        AgeRangeApi,
        ApplicationStageApi,
        DisabilityApi,
        EthnicityApi,
        FundingBodyApi,
        FundingCallTypeApi,
        FundingStreamApi,
        GenderApi,
        HowDidYouFindUsApi,
        IsCTUAlreadyInvolvedApi,
        IsCTUTeamContributionApi,
        IsFellowshipApi,
        IsInternationalMultiSiteStudyApi,
        IsLeadApplicantNHSApi,
        IsPaidRSSAdviserCoapplicantApi,
        IsPaidRSSAdviserLeadApi,
        IsPPIEAndEDIContributionApi,
        IsPrePostAwardApi,
        IsQuantativeTeamContributionApi,
        IsResubmissionApi,
        IsTeamMembersConsultedApi,
        ProfessionalBackgroundApi,
        ProjectStatusApi,
        ReportProviderApi,
        ResearcherCareerStageApi,
        ResearcherLocationApi,
        ResearcherOrganisationTypeApi,
        ResearcherRoleApi,
        ResearchMethodologyApi,
        RSSHubApi,
        SiteApi,
        SubmissionOutcomeApi,
        SubmissionStageApi,
        SupportProvidedApi,
        SupportRequestStatusApi,
        SupportTeamApi,
        TitleApi,
        UserDisciplineApi,
        WillStudyUseCTUApi,
        WorkActivityApi,
        StaticDataCache,
    ]
})
export class ApiModule { }

