import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { StaticDataApi } from './static-data.api';
import { ProjectApi } from './project.api';
import { ResearcherApi } from './researcher.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { FundingBodyApi } from './funding-body.api';
import { FundingStreamApi } from './funding-stream.api';
import { GenderApi } from './gender.api';
import { ResearchMethodologyApi } from './research-methodology.api';
import { TitleApi } from './title.api';

@NgModule({
    providers: [
        ConfigurationApi,
        StaticDataApi,
        ProjectApi,
        ResearcherApi,
        UserApi,
        UserGroupApi,
        FundingBodyApi,
        FundingStreamApi,
        GenderApi,
        ResearchMethodologyApi,
        TitleApi
    ]
})
export class ApiModule { }

