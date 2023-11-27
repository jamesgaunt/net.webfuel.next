import { Component } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ProjectExportApi } from '../../../../api/project-export.api';
import { ConfigurationService } from '../../../../core/configuration.service';

@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    public projectApi: ProjectApi,
    public staticDataCache: StaticDataCache,
    private projectExportApi: ProjectExportApi,
    private configurationService: ConfigurationService
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

  canExport() {
    return this.configurationService.hasClaim(c => c.claims.developer);
  }

  export() {
    this.projectExportApi.initialise(this.filterForm.getRawValue()).subscribe((result) => {

    });
  }
}
