import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { ProjectApi } from '../../api/project.api';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';
import { ProjectRequestComponent } from './project-request/project-request.component';
import { ProjectSupportComponent } from './project-support/project-support.component';

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
    path: 'project-support/:id',
    component: ProjectSupportComponent,
    resolve: { project: ProjectApi.projectResolver('id') },
    canDeactivate: [DeactivateService.isPristine<ProjectSupportComponent>()],
    data: { activeSideMenu: 'Projects' }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
