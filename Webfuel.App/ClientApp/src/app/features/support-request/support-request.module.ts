import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { SupportRequestRoutingModule } from './support-request-routing.module';

import { SupportRequestListComponent } from './support-request/support-request-list/support-request-list.component';
import { SupportRequestItemComponent } from './support-request/support-request-item/support-request-item.component';
import { SupportRequestTabsComponent } from './support-request/support-request-tabs/support-request-tabs.component';

import { CreateSupportRequestDialog, CreateSupportRequestDialogComponent } from './support-request/dialogs/create-support-request/create-support-request.dialog';
import { TriageSupportRequestDialog, TriageSupportRequestDialogComponent } from './support-request/dialogs/triage-support-request/triage-support-request.dialog';

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

    CreateSupportRequestDialogComponent,
    TriageSupportRequestDialogComponent,
  ],
  providers: [
    CreateSupportRequestDialog,
    TriageSupportRequestDialog
  ]
})
export class SupportRequestModule { }
