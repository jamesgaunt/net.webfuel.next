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
import { ProjectFilesComponent } from './project/project-files/project-files.component';
import { ProjectHistoryComponent } from './project/project-history/project-history.component';

import { CreateProjectDialog, CreateProjectDialogComponent } from './project/dialogs/create-project/create-project.dialog';
import { CreateProjectSupportDialog, CreateProjectSupportDialogComponent } from './project/dialogs/create-project-support/create-project-support.dialog';
import { CreateProjectSubmissionDialog, CreateProjectSubmissionDialogComponent } from './project/dialogs/create-project-submission/create-project-submission.dialog';
import { UpdateProjectSupportDialog, UpdateProjectSupportDialogComponent } from './project/dialogs/update-project-support/update-project-support.dialog';
import { UpdateProjectSubmissionDialog, UpdateProjectSubmissionDialogComponent } from './project/dialogs/update-project-submission/update-project-submission.dialog';

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
    ProjectHistoryComponent,
    ProjectFilesComponent,
    ProjectTabsComponent,

    CreateProjectDialogComponent,
    CreateProjectSupportDialogComponent,
    CreateProjectSubmissionDialogComponent,
    UpdateProjectSupportDialogComponent,
    UpdateProjectSubmissionDialogComponent,
  ],
  providers: [
    CreateProjectDialog,
    CreateProjectSupportDialog,
    CreateProjectSubmissionDialog,
    UpdateProjectSupportDialog,
    UpdateProjectSubmissionDialog
  ]
})
export class ProjectModule { }
