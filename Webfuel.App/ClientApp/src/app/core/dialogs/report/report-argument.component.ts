import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';
import { ReferenceLookup, ReportArgument, ReportFilter, ReportFilterType } from '../../../api/api.types';
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

    this.form.valueChanges.pipe(
      debounceTime(250),
      takeUntilDestroyed(this.destroyRef)
    ).subscribe(() => {

      this.argument.condition = this.form.value.condition ?? this.argument.condition;
      this.argument.guidsValue = this.form.value.guidsValue ?? this.argument.guidsValue;
      this.argument.doubleValue = this.form.value.doubleValue ?? this.argument.doubleValue;
      this.argument.stringValue = this.form.value.stringValue ?? this.argument.stringValue;
      this.argument.dateValue = this.form.value.dateValue ?? this.argument.dateValue;
    });

  }

  ReportFilterType = ReportFilterType;

  @Input({ required: true })
  argument!: ReportArgument

  form = new FormGroup({
    condition: new FormControl<number>(0, { nonNullable: true }),
    guidsValue: new FormControl<string[] | null>(null),
    doubleValue: new FormControl<number | null>(null),
    stringValue: new FormControl<string | null>(null),
    dateValue: new FormControl<string | null>(null)
  });

  setup() {
    this.form.patchValue({
      condition: this.argument.condition,
      guidsValue: this.argument.guidsValue,
      doubleValue: this.argument.doubleValue,
      stringValue: this.argument.stringValue,
      dateValue: this.argument.dateValue
    });
  }

  referenceDataSource: IDataSource<ReferenceLookup> = {
    query: (query) => this.reportDesignApi.lookupReferenceField({ query: query, fieldId: this.argument.fieldId, reportProviderId: this.argument.reportProviderId })
  }

  get unary() {
    var condition = this.argument.conditions.find(c => c.value == this.form.value.condition);
    if (condition == undefined)
      return false;
    return condition.unary;
  }
}
