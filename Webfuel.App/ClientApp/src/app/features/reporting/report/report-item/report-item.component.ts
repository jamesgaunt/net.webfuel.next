import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ReportSchema, Report, ReportDesign, ReportProvider } from 'api/api.types';
import { FormService } from 'core/form.service';
import { ReportDesignApi } from 'api/report-design.api';
import _ from 'shared/common/underscore';
import { ReportLauncherDialog } from 'core/dialogs/report/report-launcher.dialog';
import { ReportGroupApi } from 'api/report-group.api';
import { ConfirmDeleteDialog } from 'shared/dialogs/confirm-delete/confirm-delete.dialog';
import { StaticDataCache } from 'api/static-data.cache';
import { ReportProviderEnum } from 'api/api.enums';
import { UserService } from 'core/user.service';

@Component({
  selector: 'report-item',
  templateUrl: './report-item.component.html'
})
export class ReportItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public userService: UserService,
    public reportApi: ReportApi,
    public reportGroupApi: ReportGroupApi,
    public reportDesignApi: ReportDesignApi,
    private reportLauncherDialog: ReportLauncherDialog,
    private confirmDeleteDialog: ConfirmDeleteDialog,
    public staticDataCache: StaticDataCache,
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.report);
    this.reportDesignApi.getReportSchema({ reportProviderId: this.item.reportProviderId }).subscribe((result) => {
      this.reportSchema = result;
    });
    this.staticDataCache.reportProvider.query({ skip: 0, take: 1000 }).subscribe((result) => this.reportProviders = result.items);
  }

  item!: Report;

  reportProviders: ReportProvider[] = [];

  formatTitle() {
    var provider = this.reportProviders.find(p => p.id == this.item.reportProviderId);
    return "Report " + (!provider ? "" : ("(" + provider.name + ")"));
  }

  get isCustomReport() {
    return this.item.reportProviderId == ReportProviderEnum.CustomReport;
  }

  reset(item: Report) {
    this.item = item;

    this.form.patchValue(item);
    this.form.patchValue({
      customReportProvider: item.design.customReportProvider,
      customReportLauncher: item.design.customReportLauncher,
      customReportMetadata: item.design.customReportMetadata,
      customReportTemplate: item.design.customReportTemplate,
    });

    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    description: new FormControl<string>('', { nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
    isPublic: new FormControl(false, { nonNullable: true }),
    reportGroupId: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),

    // Part of the ReportDesign 
    customReportProvider: new FormControl<string>('', { nonNullable: true }),
    customReportLauncher: new FormControl<string>('', { nonNullable: true }),
    customReportMetadata: new FormControl<string>('', { nonNullable: true }),
    customReportTemplate: new FormControl<string>('', { nonNullable: true })
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    var value = this.form.getRawValue();

    value.design.customReportProvider = value.customReportProvider;
    value.design.customReportLauncher = value.customReportLauncher;
    value.design.customReportMetadata = value.customReportMetadata;
    value.design.customReportTemplate = value.customReportTemplate;

    this.reportApi.update(value, { successGrowl: "Report  Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['reporting/report-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['reporting/report-list']);
  }

  delete() {
    this.confirmDeleteDialog.open({ title: "Report " }).subscribe(() => {
      this.reportApi.delete({ id: this.item.id }, { successGrowl: "Report  Deleted" }).subscribe(() => {
        this.reset(this.item);
        this.router.navigate(['reporting/report-list']);
      });
    });
  }

  // Report Designer

  reportSchema!: ReportSchema;

  run() {
    this.reportApi.update(this.form.getRawValue(), { successGrowl: "Report Updated" }).subscribe((result) => {
      this.reset(result);
      this.reportLauncherDialog.open({ reportId: this.item.id });
    });
  }
}
