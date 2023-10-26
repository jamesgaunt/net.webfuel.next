import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';

import { ProjectListComponent } from './project/project-list/project-list.component';
import { ProjectItemComponent } from './project/project-item/project-item.component';
import { ProjectTabsComponent } from './project/project-tabs/project-tabs.component';
import { ProjectRequestComponent } from './project/project-request/project-request.component';
import { ProjectSupportComponent } from './project/project-support/project-support.component';
import { ProjectSubmissionComponent } from './project/project-submission/project-submission.component';

import { ProjectCreateDialogComponent, ProjectCreateDialogService } from './project/dialogs/project-create-dialog/project-create-dialog.component';
import { ProjectSupportCreateDialogComponent, ProjectSupportCreateDialogService } from './project/dialogs/project-support-create-dialog/project-support-create-dialog.component';
import { ProjectSupportUpdateDialogComponent, ProjectSupportUpdateDialogService } from './project/dialogs/project-support-update-dialog/project-support-update-dialog.component';
import { ProjectSubmissionCreateDialogComponent, ProjectSubmissionCreateDialogService } from './project/dialogs/project-submission-create-dialog/project-submission-create-dialog.component';
import { ProjectSubmissionUpdateDialogComponent, ProjectSubmissionUpdateDialogService } from './project/dialogs/project-submission-update-dialog/project-submission-update-dialog.component';

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
  ],
  providers: [
    ProjectCreateDialogService,
    ProjectSupportCreateDialogService,
    ProjectSupportUpdateDialogService,
    ProjectSubmissionCreateDialogService,
    ProjectSubmissionUpdateDialogService,
  ]
})
export class ProjectModule { }
