import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';
import { ReportFilter, ReportFilterType } from '../../../api/api.types';
import { FormControl, FormGroup } from '@angular/forms';
import { ReportDesignApi } from '../../../api/report-design.api';
import { IDataSource } from '../../../shared/common/data-source';

@Component({
  selector: 'report-argument',
  templateUrl: './report-argument.component.html',
})
export class ReportArgumentComponent implements OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private reportDesignApi: ReportDesignApi
  ) {
  }

  ngOnInit(): void {
    this.setup();
  }

  ReportFilterType = ReportFilterType;

  @Input({ required: true })
  reportProviderId!: string;

  @Input({ required: true })
  filter!: ReportFilter;

  form = new FormGroup({
    condition: new FormControl<number>(0, { nonNullable: true }),
    value: new FormControl<any>(null)
  });

  setup() {
    let f = <any>this.filter;

    this.form.patchValue({ condition: f.condition });
    this.form.patchValue({ value: f.value });
  }

  referenceDataSource: IDataSource<any> = {
    query: (query) => this.reportDesignApi.queryReferenceField({ query: query, fieldId: (<any>(this.filter)).fieldId, reportProviderId: this.reportProviderId })
  }
}
