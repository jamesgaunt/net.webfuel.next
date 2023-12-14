import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { ReportFilterBoolean, ReportFilterBooleanCondition } from '../../../../api/api.types';
import _ from 'shared/common/underscore';

@Component({
  selector: 'report-filter-boolean',
  templateUrl: './report-filter-boolean.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportFilterBooleanComponent),
      multi: true
    }
  ]
})
export class ReportFilterBooleanComponent implements ControlValueAccessor, OnInit {

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

  reset(filter: ReportFilterBoolean) {
    this.filter = filter;
    this.form.patchValue(filter);
  }

  filter!: ReportFilterBoolean;

  form = new FormGroup({
    name: new FormControl<string>('', { nonNullable: true }),
    editable: new FormControl<boolean>(false, { nonNullable: true }),
    condition: new FormControl<number>(ReportFilterBooleanCondition.True),
  });

  // Inputs

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.filter));
  }

  onChange: (value: ReportFilterBoolean) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportFilterBoolean): void {
    this.reset(_.deepClone(value));
  }

  public registerOnChange(fn: (value: ReportFilterBoolean) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
