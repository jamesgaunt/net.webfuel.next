import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { StaticDataApi } from './static-data.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { FundingBodyApi } from './funding-body.api';
import { FundingStreamApi } from './funding-stream.api';
import { GenderApi } from './gender.api';
import { TitleApi } from './title.api';

@NgModule({
    providers: [
        ConfigurationApi,
        StaticDataApi,
        UserApi,
        UserGroupApi,
        FundingBodyApi,
        FundingStreamApi,
        GenderApi,
        TitleApi
    ]
})
export class ApiModule { }

