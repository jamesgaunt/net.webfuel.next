import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../../shared/shared.module';
import { ProjectRoutingModule } from './project-routing.module';

import { ProjectListComponent } from './project/project-list/project-list.component';
import { ProjectItemComponent } from './project/project-item/project-item.component';
import { ProjectTabsComponent } from './project/project-tabs/project-tabs.component';
import { ProjectRequestComponent } from './project/project-request/project-request.component';
import { ProjectResearcherComponent } from './project/project-researcher/project-researcher.component';
import { ProjectSupportComponent } from './project/project-support/project-support.component';
import { ProjectSubmissionComponent } from './project/project-submission/project-submission.component';
import { ProjectFilesComponent } from './project/project-files/project-files.component';
import { ProjectHistoryComponent } from './project/project-history/project-history.component';
import { ProjectTeamSupportComponent } from './project/project-team-support/project-team-support.component';

import { CreateProjectSupportDialog, CreateProjectSupportDialogComponent } from './project/project-support/create-project-support/create-project-support.dialog';
import { UpdateProjectSupportDialog, UpdateProjectSupportDialogComponent } from './project/project-support/update-project-support/update-project-support.dialog';

import { CreateProjectSubmissionDialog, CreateProjectSubmissionDialogComponent } from './project/project-submission/create-project-submission/create-project-submission.dialog';
import { UpdateProjectSubmissionDialog, UpdateProjectSubmissionDialogComponent } from './project/project-submission/update-project-submission/update-project-submission.dialog';
import { CreateProjectTeamSupportDialog, CreateProjectTeamSupportDialogComponent } from './project/project-team-support/create-project-team-support/create-project-team-support.dialog';
import { CompleteProjectTeamSupportDialog, CompleteProjectTeamSupportDialogComponent } from './project/project-team-support/complete-project-team-support/complete-project-team-support.dialog';
import { UpdateProjectTeamSupportDialog, UpdateProjectTeamSupportDialogComponent } from './project/project-team-support/update-project-team-support/update-project-team-support.dialog';

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
    ProjectResearcherComponent,
    ProjectSupportComponent,
    ProjectSubmissionComponent,
    ProjectTeamSupportComponent,
    ProjectHistoryComponent,
    ProjectFilesComponent,
    ProjectTabsComponent,

    CreateProjectSupportDialogComponent,
    CreateProjectSubmissionDialogComponent,
    UpdateProjectSupportDialogComponent,
    UpdateProjectSubmissionDialogComponent,
    CreateProjectTeamSupportDialogComponent,
    CompleteProjectTeamSupportDialogComponent,
    UpdateProjectTeamSupportDialogComponent,
  ],
  providers: [
    CreateProjectSupportDialog,
    CreateProjectSubmissionDialog,
    UpdateProjectSupportDialog,
    UpdateProjectSubmissionDialog,
    CreateProjectTeamSupportDialog,
    CompleteProjectTeamSupportDialog,
    UpdateProjectTeamSupportDialog,
  ]
})
export class ProjectModule { }
