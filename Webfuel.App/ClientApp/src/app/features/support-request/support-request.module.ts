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

    TriageSupportRequestDialogComponent,
    SendTriageTemplateDialogComponent,
  ],
  providers: [TriageSupportRequestDialog, SendTriageTemplateDialog],
})
export class SupportRequestModule {}
