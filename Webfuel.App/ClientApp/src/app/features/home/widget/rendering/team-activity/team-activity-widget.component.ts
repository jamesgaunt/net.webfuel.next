import { DialogRef } from '@angular/cdk/dialog';
import { Component, DestroyRef, ElementRef, EventEmitter, Input, TemplateRef, ViewChild, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { DashboardMetric, TeamActivityConfig, TeamActivityData, TeamMember, Widget } from 'api/api.types';
import { StaticDataCache } from 'api/static-data.cache';
import { WidgetApi } from 'api/widget.api';
import { DialogService } from 'core/dialog.service';
import { FormService } from 'core/form.service';
import { QueryService } from 'core/query.service';
import { WidgetService } from 'core/widget.service';
import { BehaviorSubject, Observable } from 'rxjs';
import { IDataSource } from 'shared/common/data-source';
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
    private queryService: QueryService,
  ) {
  }

  @Input({ required: true })
  widget!: BehaviorSubject<Widget>;

  ngOnInit() {
    this.widget
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((result) => {
        this.data = JSON.parse(result.dataJson);
        this.dataSource.changed!.next(1);
      });
  }

  isProcessing() {
    return this.widgetService.isProcessing(this.widget.value.id);
  }

  data: TeamActivityData = {
    teamMembers: []
  };

  dataSource: IDataSource<TeamMember> = {
    query: (query) => this.queryService.query(query, this.data.teamMembers),
    changed: new EventEmitter<any>()
  }

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
    supportTeamId: new FormControl<string>(null!, { validators: [Validators.required], nonNullable: true }),
    weeks: new FormControl<number>(2, { validators: [Validators.required], nonNullable: true })
  })

  patchConfig() {
    try {
      var config = <TeamActivityConfig>JSON.parse(this.widget.value.configJson);
      this.configForm.patchValue({
        supportTeamId: config.supportTeamId,
        weeks: config.weeks
      });
    }
    catch { }
  }

  editConfig() {
    this.patchConfig();
    this.configDialogRef = this.dialogService.openTemplate(this.configDialog);
  }

  saveConfig() {
    if (this.formService.hasErrors(this.configForm))
      return;

    var configJson = JSON.stringify(this.configForm.getRawValue());

    this.widgetApi.update({ id: this.widget.value.id, configJson: configJson }, { successGrowl: "Configuration Updated" }).subscribe((widget) => {
      this.widgetService.processWidget(widget.id);
      this.cancelConfig();
    });
  }

  cancelConfig() {
    this.configDialogRef?.close();
    this.configDialogRef = undefined;
  }
}

