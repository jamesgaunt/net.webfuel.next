import { NgModule } from '@angular/core';
import { ConfigurationApi } from './configuration.api';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';

@NgModule({
    providers: [
        ConfigurationApi,
        UserApi,
        UserGroupApi
    ]
})
export class ApiModule { }

