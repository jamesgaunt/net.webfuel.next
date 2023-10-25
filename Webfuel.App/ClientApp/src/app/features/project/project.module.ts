import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';
import { ProjectTabsComponent } from './project-tabs/project-tabs.component';
import { ProjectRequestComponent } from './project-request/project-request.component';
import { ProjectSupportComponent } from './project-support/project-support.component';
import { ProjectSubmissionComponent } from './project-submission/project-submission.component';

import { ProjectCreateDialogComponent } from './project-create-dialog/project-create-dialog.component';
import { ProjectSupportCreateDialogComponent } from './project-support-create-dialog/project-support-create-dialog.component';
import { ProjectSupportUpdateDialogComponent } from './project-support-update-dialog/project-support-update-dialog.component';
import { ProjectSubmissionCreateDialogComponent } from './project-submission-create-dialog/project-submission-create-dialog.component';
import { ProjectSubmissionUpdateDialogComponent } from './project-submission-update-dialog/project-submission-update-dialog.component';

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
    ProjectSubmissionComponent,
    ProjectTabsComponent,

    ProjectCreateDialogComponent,
    ProjectSupportCreateDialogComponent,
    ProjectSupportUpdateDialogComponent,
    ProjectSubmissionCreateDialogComponent,
    ProjectSubmissionUpdateDialogComponent,
  ]
})
export class ProjectModule { }
