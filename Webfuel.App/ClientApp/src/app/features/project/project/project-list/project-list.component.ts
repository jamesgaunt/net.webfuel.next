import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfirmDeleteDialogService } from '../../../../shared/dialogs/confirm-delete/confirm-delete-dialog.component';
import { ProjectCreateDialogService } from '../dialogs/project-create-dialog/project-create-dialog.component';


@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    public projectApi: ProjectApi,
    public staticDataCache: StaticDataCache,
    private createProjectDialog: ProjectCreateDialogService,
    private confirmDeleteDialog: ConfirmDeleteDialogService
  ) {
  }

  add() {
    this.createProjectDialog.open();
  }

  edit(item: Project) {
    this.router.navigate(['project/project-item', item.id]);
  }

  delete(item: Project) {
    this.confirmDeleteDialog.open({ title: "Project" }).subscribe((result) => {
      this.projectApi.delete({ id: item.id }, { successGrowl: "Project Deleted" }).subscribe();
    });
  }
}
