import { Component, DestroyRef, Input, inject } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { FormControl, FormGroup } from '@angular/forms';
import { DashboardMetric, DashboardMetrics, Widget } from 'api/api.types';
import { debug } from 'console';
import { WidgetService } from 'core/widget.service';
import { BehaviorSubject, Observable } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'metrics-summary-widget',
  templateUrl: './metrics-summary-widget.component.html'
})
export class MetricsSummaryWidgetComponent {

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

  isProcessing() {
    return this.widgetService.isProcessing(this.widget.value.id);
  }

  data: DashboardMetrics = {
    metrics: []
  };

  routerParams(metric: DashboardMetric) {
    if (!metric.routerParams)
      return {};
    return JSON.parse(metric.routerParams);
  }
}
