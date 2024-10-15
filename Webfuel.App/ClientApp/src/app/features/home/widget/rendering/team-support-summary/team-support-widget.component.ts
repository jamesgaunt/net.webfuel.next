import { Component, DestroyRef, Input, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, TeamSupportData, Widget } from 'api/api.types';
import { WidgetService } from 'core/widget.service';
import _ from 'shared/common/underscore';

@Component({
  selector: 'team-support-widget',
  templateUrl: './team-support-widget.component.html'
})
export class TeamSupportWidgetComponent {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private widgetService: WidgetService
  ) {
  }

  @Input({ required: true })
  widget!: Widget;

  ngOnInit() {
    this.widgetService.getDataSource(this.widget)
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((result) => {
        if (result.complete) {
          this.data = JSON.parse(result.data);
        }
      });
  }

  data: TeamSupportData | null = null;

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
