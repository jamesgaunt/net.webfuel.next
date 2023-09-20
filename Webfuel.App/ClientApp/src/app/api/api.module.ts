import { NgModule } from '@angular/core';
import { UserApi } from './user.api';
import { UserGroupApi } from './user-group.api';

@NgModule({
    providers: [
        UserApi,
        UserGroupApi
    ]
})
export class ApiModule { }

