import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Project } from 'api/api.types';
import { ProjectApi } from 'api/project.api';
import { StaticDataCache } from 'api/static-data.cache';
import { ConfigurationService } from '../../../../core/configuration.service';
import { ReportService } from '../../../../core/report.service';
import { UserApi } from '../../../../api/user.api';
import { UserService } from '../../../../core/user.service';
import { ProjectStatusEnum } from '../../../../api/api.enums';
import _ from 'shared/common/underscore';
import { ReportApi } from '../../../../api/report.api';
import { ConfirmDialog } from '../../../../shared/dialogs/confirm/confirm.dialog';

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
    private reportApi: ReportApi,
    public staticDataCache: StaticDataCache,
    private configurationService: ConfigurationService,
    private reportService: ReportService,
    private confirmDialog: ConfirmDialog,
  ) {
  }

  ngAfterViewInit(): void {
    this.activatedRoute.queryParams.subscribe(params => this.applyFilterParams(params));
  }

  filterForm = new FormGroup({
    number: new FormControl<string>('', { nonNullable: true }),
    fromDate: new FormControl<string | null>(null),
    toDate: new FormControl<string | null>(null),
    statusId: new FormControl<string | null>(null),
    proposedFundingStreamId: new FormControl<string | null>(null),
    leadAdviserUserId: new FormControl<string | null>(null),
    supportAdviserUserId: new FormControl<string | null>(null),
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
      supportAdviserUserId: null,
      title: '',
      teamContactName: '',
      requestedSupportTeamId: null,
    });
  }

  edit(item: Project) {
    this.router.navigate(['project/project-item', item.id]);
  }

  export() {
    this.projectApi.export(_.merge(this.filterForm.getRawValue(), { skip: 0, take: 0 })).subscribe((result) => {
      this.reportService.runReport(result);
    });
  }

  annualReport() {
    this.confirmDialog.open({ title: "Run Experimental Annual Report", message: "This report is under development and available for testing purposes only." }).subscribe((result) => {
      this.reportApi.runAnnualReport().subscribe((result) => {
        this.reportService.runReport(result);
      });
    });
  }

  applyFilterParams(params: Params) {
    const show = params['show'];
    switch (show) {
      case 'all':
        this.resetFilterForm();
        return;

      case 'active':
        this.resetFilterForm();
        this.filterForm.patchValue({ statusId: ProjectStatusEnum.Active });
        return;
    }
    
    const supportTeam = params['supportTeam'];
    if (supportTeam) {
      this.resetFilterForm();
      this.filterForm.patchValue({ statusId: ProjectStatusEnum.Active });
      this.filterForm.patchValue({ requestedSupportTeamId: supportTeam });
    }
  }

  // Open Team Request Cache


}
