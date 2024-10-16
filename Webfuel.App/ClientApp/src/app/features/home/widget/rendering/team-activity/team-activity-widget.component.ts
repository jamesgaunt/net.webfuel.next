import { DialogRef } from '@angular/cdk/dialog';
import { Component, DestroyRef, Input, TemplateRef, ViewChild, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DashboardMetric, TeamActivityConfig, TeamActivityData, Widget } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { WidgetApi } from 'api/widget.api';
import { DialogService } from 'core/dialog.service';
import { FormService } from 'core/form.service';
import { WidgetService } from 'core/widget.service';
import { Observable } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'team-activity-widget',
  templateUrl: './team-activity-widget.component.html'
})
export class TeamActivityWidgetComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private widgetService: WidgetService,
    private dialogService: DialogService,
    public staticDataCache: StaticDataCache,
    private formService: FormService,
    private widgetApi: WidgetApi,
  ) {
  }

  @Input({ required: true })
  widget!: Observable<Widget>;

  ngOnInit() {
    this.widget
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((result) => {
        this.data = JSON.parse(result.dataJson);
      });
  }

  data: TeamActivityData = {
    teamMembers: []
  };

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }

  // Config

  @ViewChild('configDialog', { static: false })
  private configDialog!: TemplateRef<any>;
  private configDialogRef: DialogRef | undefined;

  configForm = new FormGroup({
    supportTeamId: new FormControl<string | null>(null, { validators: [Validators.required]}),
  })

  editConfig() {
    this.configDialogRef = this.dialogService.openTemplate(this.configDialog);
  }

  saveConfig() {
    if (this.formService.hasErrors(this.configForm))
      return;

    var configData = JSON.stringify(this.configForm.getRawValue());

    //this.widgetApi.update({ id: this.widget.value.id, configData: configData }, { successGrowl: "Configuration Updated " }).subscribe((widget) => {
    //  this.cancelConfig();
    //});
  }

  cancelConfig() {
    this.configDialogRef?.close();
    this.configDialogRef = undefined;
  }
}

