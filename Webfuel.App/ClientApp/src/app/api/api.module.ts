import { NgModule } from '@angular/core';
import { TenantApi } from './tenant.api';
import { TenantDomainApi } from './tenant-domain.api';
import { WidgetApi } from './widget.api';

@NgModule({
    providers: [
        TenantApi,
        TenantDomainApi,
        WidgetApi
    ]
})
export class ApiModule { }

