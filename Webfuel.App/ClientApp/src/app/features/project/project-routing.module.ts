import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { ProjectApi } from '../../api/project.api';

import { ProjectListComponent } from './project/project-list/project-list.component';
import { ProjectItemComponent } from './project/project-item/project-item.component';
import { ProjectRequestComponent } from './project/project-request/project-request.component';
import { ProjectSupportComponent } from './project/project-support/project-support.component';
import { ProjectSubmissionComponent } from './project/project-submission/project-submission.component';
import { ProjectFilesComponent } from './project/project-files/project-files.component';
import { ProjectHistoryComponent } from './project/project-history/project-history.component';
import { ProjectResearcherComponent } from './project/project-researcher/project-researcher.component';

const routes: Routes = [
  {
    path: 'project-list',
    component: ProjectListComponent,
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-item/:id',
    component: ProjectItemComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectItemComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-request/:id',
    component: ProjectRequestComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectRequestComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-researcher/:id',
    component: ProjectResearcherComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectResearcherComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-support/:id',
    component: ProjectSupportComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-submission/:id',
    component: ProjectSubmissionComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectSubmissionComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-files/:id',
    component: ProjectFilesComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectFilesComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
  {
    path: 'project-history/:id',
    component: ProjectHistoryComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectHistoryComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
