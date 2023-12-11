import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { ReportFilterString } from '../../../api/api.types';
import _ from 'shared/common/underscore';
import { ReportFilterStringCondition } from '../../../api/api.enums';

@Component({
  selector: 'report-filter-string',
  templateUrl: './report-filter-string.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportFilterStringComponent),
      multi: true
    }
  ]
})
export class ReportFilterStringComponent implements ControlValueAccessor, OnInit {

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

  reset(filter: ReportFilterString) {
    this.filter = filter;
    this.form.patchValue(filter);
  }

  ReportFilterStringCondition = ReportFilterStringCondition;

  filter!: ReportFilterString;

  form = new FormGroup({
    condition: new FormControl<ReportFilterStringCondition>(ReportFilterStringCondition.Contains),
    value: new FormControl<string>('')
  });

  // Inputs

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.filter));
  }

  onChange: (value: ReportFilterString) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportFilterString): void {
    this.reset(_.deepClone(value));
  }

  public registerOnChange(fn: (value: ReportFilterString) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
