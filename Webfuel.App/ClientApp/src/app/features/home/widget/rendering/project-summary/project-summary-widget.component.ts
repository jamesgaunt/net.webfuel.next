import { Component, DestroyRef, Input, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, ProjectSummaryData, Widget } from 'api/api.types';
import { WidgetProviderApi } from 'api/widget-provider.api';
import _ from 'shared/common/underscore';

@Component({
  selector: 'project-summary-widget',
  templateUrl: './project-summary-widget.component.html'
})
export class ProjectSummaryWidgetComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private widgetProviderApi: WidgetProviderApi
  ) {
  }

  @Input({ required: true })
  widget!: Widget;

  ngOnInit() {
    this.loadData();
  }

  data: ProjectSummaryData | null = null;

  loadData() {
    this.widgetProviderApi.projectSummary({ id: this.widget.id }).subscribe(result => {
      this.data = result;
      console.log(result);
    });
  }

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
