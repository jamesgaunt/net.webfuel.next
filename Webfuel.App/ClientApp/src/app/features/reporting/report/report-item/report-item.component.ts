import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReportApi } from 'api/report.api';
import { ReportSchema, Report, ReportDesign } from '../../../../api/api.types';
import { FormService } from '../../../../core/form.service';
import { ReportDesignApi } from '../../../../api/report-design.api';
import _ from 'shared/common/underscore';
import { ReportLauncherDialog } from '../../../../core/dialogs/report/report-launcher.dialog';

@Component({
  selector: 'report-item',
  templateUrl: './report-item.component.html'
})
export class ReportItemComponent implements OnInit {

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formService: FormService,
    public reportApi: ReportApi,
    public reportDesignApi: ReportDesignApi,
    private reportLauncherDialog: ReportLauncherDialog
  ) {
  }

  ngOnInit() {
    this.reset(this.route.snapshot.data.report);
    this.reportDesignApi.getReportSchema({ reportProviderId: this.item.reportProviderId }).subscribe((result) => {
      this.reportSchema = result;
    });
  }

  item!: Report;

  reset(item: Report) {
    this.item = item;
    this.form.patchValue(item);
    this.form.markAsPristine();
  }

  form = new FormGroup({
    id: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    name: new FormControl<string>('', { validators: [Validators.required], nonNullable: true }),
    design: new FormControl<ReportDesign>(null!, { validators: [Validators.required], nonNullable: true }),
  });

  save(close: boolean) {
    if (this.formService.hasErrors(this.form))
      return;

    this.reportApi.update(this.form.getRawValue(), { successGrowl: "Report  Updated" }).subscribe((result) => {
      this.reset(result);
      if(close)
        this.router.navigate(['reporting/report-list']);
    });
  }

  cancel() {
    this.reset(this.item);
    this.router.navigate(['reporting/report-list']);
  }

  // Report Designer

  reportSchema!: ReportSchema;

  run() {
    this.reportApi.update(this.form.getRawValue(), { successGrowl: "Report  Updated" }).subscribe((result) => {
      this.reset(result);
      this.reportLauncherDialog.open({ reportId: this.item.id });
    });
  }
}
