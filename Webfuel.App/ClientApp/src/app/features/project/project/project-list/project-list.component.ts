import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ProjectExportApi } from '../../../../api/project-export.api';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { UserApi } from '../../../../api/user.api';
import { UserService } from '../../../../core/user.service';

@Component({
  selector: 'project-list',
  templateUrl: './project-list.component.html'
})
export class ProjectListComponent {
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    public projectApi: ProjectApi,
    public userApi: UserApi,
    public userService: UserService,
    public staticDataCache: StaticDataCache,
    private projectExportApi: ProjectExportApi,
    private configurationService: ConfigurationService,
    private reportService: ReportService,
  ) {
  }

  ngAfterViewInit(): void {
    this.activatedRoute.queryParams.subscribe(params => {
      const supportTeam = params['supportTeam'];
      if (supportTeam) {
        this.filterForm.patchValue({ requestedSupportTeamId: supportTeam });
      }
    })
  }

  filterForm = new FormGroup({
    number: new FormControl<string>('', { nonNullable: true }),
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    statusId: new FormControl<string | null>(null),
    proposedFundingStreamId: new FormControl<string | null>(null),
    leadAdviserUserId: new FormControl<string | null>(null),
    title: new FormControl<string>('', { nonNullable: true }),
    teamContactName: new FormControl<string>('', { nonNullable: true }),
    requestedSupportTeamId: new FormControl<string | null>(null),
  });

  resetFilterForm() {
    this.filterForm.patchValue({
      number: '',
      fromDate: null,
      toDate: null,
      statusId: null,
      proposedFundingStreamId: null,
      leadAdviserUserId: null,
      title: '',
      teamContactName: '',
      requestedSupportTeamId: null,
    });
  }

  edit(item: Project) {
    this.router.navigate(['project/project-item', item.id]);
  }

  canExport() {
    return this.configurationService.hasClaim(c => c.claims.developer);
  }

  export() {
    this.projectExportApi.initialiseReport(this.filterForm.getRawValue()).subscribe((result) => {
      this.reportService.runReport(result);
    });
  }
}
