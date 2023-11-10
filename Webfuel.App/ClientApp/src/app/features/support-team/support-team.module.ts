import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { SupportTeamRoutingModule } from './support-team-routing.module';

import { SupportTeamListComponent } from './support-team/support-team-list/support-team-list.component';
import { SupportTeamItemComponent } from './support-team/support-team-item/support-team-item.component';
import { SupportTeamTabsComponent } from './support-team/support-team-tabs/support-team-tabs.component';

import { CreateSupportTeamDialog, CreateSupportTeamDialogComponent } from './support-team/dialogs/create-support-team/create-support-team.dialog';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    SupportTeamRoutingModule
  ],
  declarations: [
    SupportTeamListComponent,
    SupportTeamItemComponent,
    SupportTeamTabsComponent,

    CreateSupportTeamDialogComponent,
  ],
  providers: [
    CreateSupportTeamDialog,
  ]
})
export class SupportTeamModule { }
