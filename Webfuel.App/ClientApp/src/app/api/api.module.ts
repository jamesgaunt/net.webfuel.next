import { NgModule } from '@angular/core';
import { TenantApi } from './tenant.api';
import { TenantDomainApi } from './tenant-domain.api';

@NgModule({
    providers: [
        TenantApi,
        TenantDomainApi
    ]
})
export class ApiModule { }

