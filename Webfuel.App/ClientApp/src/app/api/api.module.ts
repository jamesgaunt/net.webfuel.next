import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { StaticDataApi } from './static-data.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';
import { FundingStreamApi } from './funding-stream.api';
import { TitleApi } from './title.api';

@NgModule({
    providers: [
        ConfigurationApi,
        StaticDataApi,
        UserApi,
        UserGroupApi,
        FundingStreamApi,
        TitleApi
    ]
})
export class ApiModule { }

