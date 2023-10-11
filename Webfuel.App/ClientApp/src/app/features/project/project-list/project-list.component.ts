import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { DialogService } from 'core/dialog.service';
import { GridDataSource } from '../../../shared/data-source/grid-data-source';
import _ from '../../../shared/underscore';
import { Project } from '../../../api/api.types';
import { ProjectApi } from '../../../api/project.api';
import { ProjectCreateDialogComponent } from '../project-create-dialog/project-create-dialog.component';


@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    private dialogService: DialogService,
    private projectApi: ProjectApi
  ) {
  }

  filterForm = new FormGroup({
    search: new FormControl('', { nonNullable: true })
  });

  dataSource = new GridDataSource<Project>({
    fetch: (query) => this.projectApi.queryProject(_.merge(query, this.filterForm.getRawValue())),
    filterGroup: this.filterForm
  });

  add() {
    this.dialogService.open(ProjectCreateDialogComponent, {
      callback: () => this.dataSource.fetch()
    });
  }

  edit(item: Project) {
    this.router.navigate(['project/project-item', item.id]);
  }

  delete(item: Project) {
    this.dialogService.confirmDelete({
      confirmedCallback: () => {
        this.projectApi.deleteProject({ id: item.id }, { successGrowl: "Project Deleted" }).subscribe((result) => {
          this.dataSource.fetch();
        })
      }
    });
  }
}
