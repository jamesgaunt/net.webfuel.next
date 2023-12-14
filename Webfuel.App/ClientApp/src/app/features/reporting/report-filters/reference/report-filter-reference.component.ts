import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { ReportFilterEditability, ReportFilterReference, ReportSchema } from '../../../../api/api.types';
import _ from 'shared/common/underscore';
import { ReportFilterReferenceCondition } from '../../../../api/api.enums';
import { ReportDesignApi } from '../../../../api/report-design.api';
import { IDataSource } from 'shared/common/data-source';

@Component({
  selector: 'report-filter-reference',
  templateUrl: './report-filter-reference.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportFilterReferenceComponent),
      multi: true
    }
  ]
})
export class ReportFilterReferenceComponent implements ControlValueAccessor, OnInit {

  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private readonly reportDesignApi: ReportDesignApi
  ) {
  }

  ngOnInit(): void {
    this.form.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => {
          this.filter = _.merge(this.filter, value);
          this.emitChanges()
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  reset(filter: ReportFilterReference) {
    this.filter = filter;
    this.form.patchValue(filter);
  }

  referenceDataSource: IDataSource<any> = {
    query: (query) => this.reportDesignApi.queryReferenceField({ query: query, fieldId: this.filter.fieldId, reportProviderId: this.schema.reportProviderId })
  }

  ReportFilterReferenceCondition = ReportFilterReferenceCondition;

  ReportFilterEditability = ReportFilterEditability;

  filter!: ReportFilterReference;

  form = new FormGroup({
    name: new FormControl<string>('', { nonNullable: true }),
    condition: new FormControl<ReportFilterReferenceCondition>(ReportFilterReferenceCondition.OneOf, { validators: [Validators.required], nonNullable: true }),
    value: new FormControl<string[]>([]),
    editability: new FormControl<ReportFilterEditability>(ReportFilterEditability.None, { nonNullable: true })
  });

  // Inputs

  @Input({ required: true })
  schema!: ReportSchema;

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.filter));
  }

  onChange: (value: ReportFilterReference) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportFilterReference): void {
    this.reset(_.deepClone(value));
  }

  public registerOnChange(fn: (value: ReportFilterReference) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
