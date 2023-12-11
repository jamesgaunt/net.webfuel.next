import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { ReportFilterNumber } from '../../../api/api.types';
import _ from 'shared/common/underscore';
import { ReportFilterNumberCondition } from '../../../api/api.enums';

@Component({
  selector: 'report-filter-number',
  templateUrl: './report-filter-number.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportFilterNumberComponent),
      multi: true
    }
  ]
})
export class ReportFilterNumberComponent implements ControlValueAccessor, OnInit {

  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
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

  reset(filter: ReportFilterNumber) {
    this.filter = filter;
    this.form.patchValue(filter);
  }

  ReportFilterNumberCondition = ReportFilterNumberCondition;

  filter!: ReportFilterNumber;

  form = new FormGroup({
    condition: new FormControl<ReportFilterNumberCondition>(ReportFilterNumberCondition.EqualTo),
    value: new FormControl<number | null>(null)
  });

  // Inputs

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.filter));
  }

  onChange: (value: ReportFilterNumber) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportFilterNumber): void {
    this.reset(_.deepClone(value));
  }

  public registerOnChange(fn: (value: ReportFilterNumber) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
