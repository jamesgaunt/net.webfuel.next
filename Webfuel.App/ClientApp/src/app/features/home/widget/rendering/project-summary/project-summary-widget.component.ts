import { Component, DestroyRef, Input, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, ProjectSummaryData, Widget } from 'api/api.types';
import { debug } from 'console';
import { WidgetService } from 'core/widget.service';
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

  data: ProjectSummaryData = {
    projectMetrics: []
  };

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
