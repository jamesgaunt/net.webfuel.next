import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { SupportRequestRoutingModule } from './support-request-routing.module';

import { SupportRequestListComponent } from './support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request-item/support-request-item.component';
import { SupportRequestCreateDialogComponent } from './support-request-create-dialog/support-request-create-dialog.component';
import { SupportRequestTabsComponent } from './support-request-tabs/support-request-tabs.component';
import { SupportRequestTriageDialogComponent } from './support-request-triage-dialog/support-request-triage-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    SupportRequestRoutingModule
  ],
  declarations: [
    SupportRequestListComponent,
    SupportRequestItemComponent,
    SupportRequestCreateDialogComponent,
    SupportRequestTabsComponent,
    SupportRequestTriageDialogComponent,
  ]
})
export class SupportRequestModule { }
