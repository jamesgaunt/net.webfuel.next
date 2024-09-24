import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'number-input',
  templateUrl: './number-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => NumberInputComponent),
      multi: true
    }
  ]
})
export class NumberInputComponent implements ControlValueAccessor, OnInit {

  formControl = new FormControl<string | null>(null);

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
  ) {
  }

  ngOnInit(): void {
  }

  @Input()
  placeholder = "";

  onFocus() {
    
  }

  onBlur(): void {
    var num = this.toNum(this.formControl.getRawValue());
    this.formControl.setValue(_.formatNumber(num, 0))
    this.onChange(num);   
    this.onTouched();
  }

  // Helpers

  toTxt(value: any) {
    if (value == null)
      return null;
    return _.formatNumber(_.parseNumber(value, false), 0);
  }

  toNum(value: any) {
    if (value == null)
      return null;
    return _.parseNumber(value, false);
  }

  // ControlValueAccessor API

  onChange: (value: number | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: any): void {
    this.formControl.setValue(this.toTxt(value), { emitEvent: false });
  }

  public registerOnChange(fn: (value: number | null) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.formControl.disable() : this.formControl.enable();
  }
}
