import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfirmDeleteDialog } from '../../../../shared/dialogs/confirm-delete/confirm-delete.dialog';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    public projectApi: ProjectApi,
    public staticDataCache: StaticDataCache,
    private confirmDeleteDialog: ConfirmDeleteDialog
  ) {
  }

  filterForm = new FormGroup({
    number: new FormControl<string>('', { nonNullable: true }),
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    statusId: new FormControl<string | null>(null),
    fundingStreamId: new FormControl<string | null>(null),
    title: new FormControl<string>('', { nonNullable: true })
  });

  resetFilterForm() {
    this.filterForm.patchValue({
      number: '',
      fromDate: null,
      toDate: null,
      statusId: null,
      fundingStreamId: null,
      title: ''
    })
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
