import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { SupportRequestRoutingModule } from './support-request-routing.module';

import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestTabsComponent } from './support-request/support-request-tabs/support-request-tabs.component';

import { SupportRequestTriageDialogComponent, SupportRequestTriageDialogService } from './support-request/dialogs/support-request-triage-dialog/support-request-triage-dialog.component';
import { SupportRequestCreateDialogComponent, SupportRequestCreateDialogService } from './support-request/dialogs/support-request-create-dialog/support-request-create-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    SupportRequestRoutingModule
  ],
  declarations: [
    SupportRequestListComponent,
    SupportRequestItemComponent,
    SupportRequestTabsComponent,

    SupportRequestCreateDialogComponent,
    SupportRequestTriageDialogComponent,
  ],
  providers: [
    SupportRequestCreateDialogService,
    SupportRequestTriageDialogService
  ]
})
export class SupportRequestModule { }
