import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { StaticDataApi } from './static-data.api';
import { ProjectApi } from './project.api';
import { ProjectSubmissionApi } from './project-submission.api';
import { ProjectSupportApi } from './project-support.api';
import { ResearcherApi } from './researcher.api';
import { SupportRequestApi } from './support-request.api';
import { UserActivityApi } from './user-activity.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { UserLoginApi } from './user-login.api';
import { ApplicationStageApi } from './application-stage.api';
import { FundingBodyApi } from './funding-body.api';
import { FundingCallTypeApi } from './funding-call-type.api';
import { FundingStreamApi } from './funding-stream.api';
import { GenderApi } from './gender.api';
import { HowDidYouFindUsApi } from './how-did-you-find-us.api';
import { IsCTUTeamContributionApi } from './is-ctuteam-contribution.api';
import { IsFellowshipApi } from './is-fellowship.api';
import { IsInternationalMultiSiteStudyApi } from './is-international-multi-site-study.api';
import { IsLeadApplicantNHSApi } from './is-lead-applicant-nhs.api';
import { IsPPIEAndEDIContributionApi } from './is-ppieand-edicontribution.api';
import { IsQuantativeTeamContributionApi } from './is-quantative-team-contribution.api';
import { IsResubmissionApi } from './is-resubmission.api';
import { IsTeamMembersConsultedApi } from './is-team-members-consulted.api';
import { ProjectStatusApi } from './project-status.api';
import { ResearchMethodologyApi } from './research-methodology.api';
import { SiteApi } from './site.api';
import { SubmissionOutcomeApi } from './submission-outcome.api';
import { SubmissionStageApi } from './submission-stage.api';
import { SupportProvidedApi } from './support-provided.api';
import { SupportRequestStatusApi } from './support-request-status.api';
import { TitleApi } from './title.api';
import { UserDisciplineApi } from './user-discipline.api';
import { WorkActivityApi } from './work-activity.api';
import { StaticDataCache } from './static-data.cache';

@NgModule({
    providers: [
        ConfigurationApi,
        StaticDataApi,
        ProjectApi,
        ProjectSubmissionApi,
        ProjectSupportApi,
        ResearcherApi,
        SupportRequestApi,
        UserActivityApi,
        UserApi,
        UserGroupApi,
        UserLoginApi,
        ApplicationStageApi,
        FundingBodyApi,
        FundingCallTypeApi,
        FundingStreamApi,
        GenderApi,
        HowDidYouFindUsApi,
        IsCTUTeamContributionApi,
        IsFellowshipApi,
        IsInternationalMultiSiteStudyApi,
        IsLeadApplicantNHSApi,
        IsPPIEAndEDIContributionApi,
        IsQuantativeTeamContributionApi,
        IsResubmissionApi,
        IsTeamMembersConsultedApi,
        ProjectStatusApi,
        ResearchMethodologyApi,
        SiteApi,
        SubmissionOutcomeApi,
        SubmissionStageApi,
        SupportProvidedApi,
        SupportRequestStatusApi,
        TitleApi,
        UserDisciplineApi,
        WorkActivityApi,
        StaticDataCache,
    ]
})
export class ApiModule { }

