import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';
import { ProjectTabsComponent } from './project-tabs/project-tabs.component';
import { ProjectRequestComponent } from './project-request/project-request.component';
import { ProjectSupportComponent } from './project-support/project-support.component';

import { ProjectCreateDialogComponent } from './project-create-dialog/project-create-dialog.component';
import { ProjectSupportCreateDialogComponent } from './project-support-create-dialog/project-support-create-dialog.component';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ProjectRoutingModule
  ],
  declarations: [
    ProjectListComponent,
    ProjectItemComponent,
    ProjectRequestComponent,
    ProjectSupportComponent,
    ProjectTabsComponent,

    ProjectCreateDialogComponent,
    ProjectSupportCreateDialogComponent
  ]
})
export class ProjectModule { }
