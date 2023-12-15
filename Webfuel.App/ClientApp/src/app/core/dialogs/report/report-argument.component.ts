import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';
import { ReportArgument, ReportFilter, ReportFilterType } from '../../../api/api.types';
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

  @Input({ required: true })
  argument!: ReportArgument

  form = new FormGroup({
    condition: new FormControl<number>(0, { nonNullable: true }),
    value: new FormControl<any>(null)
  });

  setup() {
    this.form.patchValue({ condition: this.argument.condition });
  }

  referenceDataSource: IDataSource<any> = {
    query: (query) => this.reportDesignApi.queryReferenceField({ query: query, fieldId: this.argument.fieldId, reportProviderId: this.argument.reportProviderId })
  }
}
