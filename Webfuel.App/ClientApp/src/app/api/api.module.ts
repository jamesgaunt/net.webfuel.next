import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { StaticDataApi } from './static-data.api';
import { ProjectApi } from './project.api';
import { ResearcherApi } from './researcher.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { ApplicationStageApi } from './application-stage.api';
import { FundingBodyApi } from './funding-body.api';
import { FundingCallTypeApi } from './funding-call-type.api';
import { FundingStreamApi } from './funding-stream.api';
import { GenderApi } from './gender.api';
import { ProjectStatusApi } from './project-status.api';
import { ResearchMethodologyApi } from './research-methodology.api';
import { SubmissionStageApi } from './submission-stage.api';
import { SupportRequestStatusApi } from './support-request-status.api';
import { TitleApi } from './title.api';
import { StaticDataCache } from './static-data.cache';

@NgModule({
    providers: [
        ConfigurationApi,
        StaticDataApi,
        ProjectApi,
        ResearcherApi,
        UserApi,
        UserGroupApi,
        ApplicationStageApi,
        FundingBodyApi,
        FundingCallTypeApi,
        FundingStreamApi,
        GenderApi,
        ProjectStatusApi,
        ResearchMethodologyApi,
        SubmissionStageApi,
        SupportRequestStatusApi,
        TitleApi,
        StaticDataCache,
    ]
})
export class ApiModule { }

