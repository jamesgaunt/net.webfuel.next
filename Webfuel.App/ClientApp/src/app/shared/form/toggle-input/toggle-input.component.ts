import { Input, Output, Component, EventEmitter, HostListener, forwardRef, Optional, Self, ChangeDetectorRef, SimpleChanges, ChangeDetectionStrategy } from '@angular/core';
import { NG_VALUE_ACCESSOR, ControlValueAccessor, NgControl } from '@angular/forms';
import { noop } from 'rxjs';

@Component({
  selector: 'toggle-input',
  templateUrl: './toggle-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ToggleInputComponent),
      multi: true
    }
  ]
})
export class ToggleInputComponent implements ControlValueAccessor {

  constructor(
  ) {
  }

  // Inputs

  @Input()
  label: string = "";

  // Value

  get value() {
    return this._value;
  }
  set value(value) {
    this._value = value;
  }
  _value: boolean = false;

  toggle() {
    if (this.isDisabled)
      return;
    this.value = !this.value;
    this.onChange(this.value);
    this.onTouched();
  }

  // ControlValueAccessor

  onChange: (value: boolean) => void = noop;
  onTouched: () => void = noop;
  isDisabled: boolean = false;

  writeValue(obj: any): void {
    this.value = obj === true;
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }

  setDisabledState(isDisabled: boolean): void {
    this.isDisabled = isDisabled;
  }
}
