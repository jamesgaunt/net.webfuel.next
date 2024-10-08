import { Component, DestroyRef, Input, inject } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, TeamSupportData, Widget } from 'api/api.types';
import { WidgetProviderApi } from 'api/widget-provider.api';
import _ from 'shared/common/underscore';

@Component({
  selector: 'team-support-widget',
  templateUrl: './team-support-widget.component.html'
})
export class TeamSupportWidgetComponent {

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

  data: TeamSupportData | null = null;

  loadData() {
    this.widgetProviderApi.teamSupport({ id: this.widget.id }).subscribe(result => {
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
