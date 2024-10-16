import { Component, DestroyRef, Input, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, TeamSupportData, Widget } from 'api/api.types';
import { WidgetService } from 'core/widget.service';
import { BehaviorSubject, Observable } from 'rxjs';
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
  widget!: BehaviorSubject<Widget>;

  ngOnInit() {
    this.widget
      .pipe(
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((result) => {
        this.data = JSON.parse(result.dataJson);
      });
  }

  data: TeamSupportData = {
    supportMetrics: []
  };

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
