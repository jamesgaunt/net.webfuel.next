import { NgModule } from '@angular/core';
import { TenantApi } from './tenant.api';
import { TenantDomainApi } from './tenant-domain.api';
import { UserApi } from './user.api';

@NgModule({
    providers: [
        TenantApi,
        TenantDomainApi,
        UserApi
    ]
})
export class ApiModule { }

