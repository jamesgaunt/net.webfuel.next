import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { Project } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { StaticDataCache } from '../../../api/static-data.cache';
import { ProjectCreateDialogComponent } from '../project-create-dialog/project-create-dialog.component';


@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    public projectApi: ProjectApi,
    public staticDataCache: StaticDataCache
  ) {
  }

  add() {
    this.dialogService.open(ProjectCreateDialogComponent, {
    });
  }

  edit(item: Project) {
    this.router.navigate(['project/project-item', item.id]);
  }

  delete(item: Project) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.projectApi.delete({ id: item.id }, { successGrowl: "Project Deleted" }).subscribe((result) => {
        })
      }
    });
  }
}
