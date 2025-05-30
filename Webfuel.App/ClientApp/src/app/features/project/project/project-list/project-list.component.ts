import { Component } from '@angular/core';
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
import { RunAnnualReportDialog } from '../../../../shared/dialogs/run-annual-report/run-annual-report.dialog';

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
    private runAnnualReportDialog: RunAnnualReportDialog,
    public staticDataCache: StaticDataCache,
    private configurationService: ConfigurationService,
    private reportService: ReportService,
    private confirmDialog: ConfirmDialog,
  ) {
  }

  get developer() { return this.configurationService.hasClaim(p => p.claims.developer); }

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
    openSupportTeamId: new FormControl<string | null>(null),
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
      openSupportTeamId: null,
    });
  }

  createTest() {
    this.projectApi.createTest({}).subscribe(() => {
      this.projectApi.changed.emit();
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
    this.runAnnualReportDialog.open({}).subscribe((result) => {
    });
  }

  applyFilterParams(params: Params) {
    const show = params['show'];
    const supportTeam = params['supportTeam'];
    const leadAdviser = params['leadAdviser'];
    const supportAdviser = params['supportAdviser'];

    if (show || supportTeam || leadAdviser || supportAdviser)
      this.resetFilterForm();

    switch (show) {
      case 'active':
        this.filterForm.patchValue({ statusId: ProjectStatusEnum.Active });
        break;

      case 'on-hold':
        this.filterForm.patchValue({ statusId: ProjectStatusEnum.OnHold });
        break;

      case 'submitted-on-hold':
        this.filterForm.patchValue({ statusId: ProjectStatusEnum.SubmittedOnHold });
        break;
    }
    
    if (supportTeam)
      this.filterForm.patchValue({ openSupportTeamId: supportTeam });

    if (leadAdviser)
      this.filterForm.patchValue({ leadAdviserUserId: leadAdviser });

    if (supportAdviser)
      this.filterForm.patchValue({ supportAdviserUserId: supportAdviser });
  }

  clipTitle(title: string) {
    if (title.length > 50)
      return title.substring(0, 50) + '...';
    return title;
  }
}
