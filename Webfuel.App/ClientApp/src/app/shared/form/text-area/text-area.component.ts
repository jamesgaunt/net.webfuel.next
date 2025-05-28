import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, inject, Input, OnInit, HostBinding } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';
import { __makeTemplateObject } from 'tslib';

@Component({
  selector: 'text-area',
  templateUrl: './text-area.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => TextAreaComponent),
      multi: true
    }
  ]
})
export class TextAreaComponent implements ControlValueAccessor, OnInit {

  @HostBinding('class.control-host') host = true;

  formControl: FormControl = new FormControl<string>('');

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
  ) {
  }

  ngOnInit(): void {
    this.formControl.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => {
          this.onChange(value);
          console.log("Emitting: " + value);
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  // Inputs

  @Input()
  placeholder = "";

  @Input()
  set maxlength(value: string | number | null) {
    this._maxlength = _.parseNumber(value, false);
  }
  _maxlength: number | null = null;

  get remaining(): number {
    if (this._maxlength === null)
      return 10000;
    return this._maxlength - ('' + this.formControl.value).length;
  }

  public onBlur(): void {
    this.onTouched();
  }

  // ControlValueAccessor API

  onChange: (value: string) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: string): void {
    this.formControl.setValue(value, { emitEvent: false });
  }

  public registerOnChange(fn: (value: string) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.formControl.disable({ emitEvent: false }) : this.formControl.enable({ emitEvent: false });
  }
}
