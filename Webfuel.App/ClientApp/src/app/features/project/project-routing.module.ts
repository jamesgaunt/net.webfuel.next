import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DeactivateService } from '../../core/deactivate.service';
import { ProjectApi } from '../../api/project.api';

import { ProjectListComponent } from './project-list/project-list.component';
import { ProjectItemComponent } from './project-item/project-item.component';

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
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProjectRoutingModule { }
