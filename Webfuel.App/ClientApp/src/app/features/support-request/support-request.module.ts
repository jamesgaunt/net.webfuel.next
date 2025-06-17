import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { SharedModule } from '../../shared/shared.module';
import { SupportRequestRoutingModule } from './support-request-routing.module';

import { SupportRequestFilesComponent } from './support-request/support-request-files/support-request-files.component';
import { SupportRequestHistoryComponent } from './support-request/support-request-history/support-request-history.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestResearcherComponent } from './support-request/support-request-researcher/support-request-researcher.component';
import { SupportRequestTabsComponent } from './support-request/support-request-tabs/support-request-tabs.component';

import {
  SendTriageTemplateDialog,
  SendTriageTemplateDialogComponent,
} from './support-request/dialogs/send-triage-template/send-triage-template.dialog';
import {
  TriageSupportRequestDialog,
  TriageSupportRequestDialogComponent,
} from './support-request/dialogs/triage-support-request/triage-support-request.dialog';
import { SupportRequestPrintoutComponent } from './support-request/support-request-printout/support-request-printout.component';

import {
  CreateSupportRequestSupportDialog,
  CreateSupportRequestSupportDialogComponent,
} from './support-request/support-request-support/create-support-request-support/create-support-request-support.dialog';
import {
  UpdateSupportRequestSupportDialog,
  UpdateSupportRequestSupportDialogComponent,
} from './support-request/support-request-support/update-support-request-support/update-support-request-support.dialog';
import { SupportRequestSupportComponent } from './support-request/support-request-support/support-request-support.component';

@NgModule({
  imports: [CommonModule, SharedModule, SupportRequestRoutingModule],
  declarations: [
    SupportRequestListComponent,
    SupportRequestItemComponent,
    SupportRequestTabsComponent,
    SupportRequestFilesComponent,
    SupportRequestResearcherComponent,
    SupportRequestHistoryComponent,
    SupportRequestPrintoutComponent,
    SupportRequestSupportComponent,

    CreateSupportRequestSupportDialogComponent,
    UpdateSupportRequestSupportDialogComponent,
    TriageSupportRequestDialogComponent,
    SendTriageTemplateDialogComponent,
  ],
  providers: [TriageSupportRequestDialog, SendTriageTemplateDialog, CreateSupportRequestSupportDialog, UpdateSupportRequestSupportDialog],
})
export class SupportRequestModule {}
