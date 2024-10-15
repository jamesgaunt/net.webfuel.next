import { Component, DestroyRef, Input, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, ProjectSummaryData, Widget } from 'api/api.types';
import { debug } from 'console';
import { WidgetDataSource, WidgetService } from 'core/widget.service';
import { BehaviorSubject, Observable } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'project-summary-widget',
  templateUrl: './project-summary-widget.component.html'
})
export class ProjectSummaryWidgetComponent {

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
          console.log(this.data);
        }
      });
  }

  data: ProjectSummaryData | null = null;

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
