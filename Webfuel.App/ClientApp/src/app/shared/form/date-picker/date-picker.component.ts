import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, forwardRef, inject, Input, OnInit } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import { DialogService } from '../../../core/dialog.service';
import { Day } from '../date-calendar/Day';
import { DatePickerDialogService } from '../../dialogs/date-picker-dialog/date-picker-dialog.component';

@Component({
  selector: 'date-picker',
  templateUrl: './date-picker.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DatePickerComponent),
      multi: true
    }
  ]
})
export class DatePickerComponent implements ControlValueAccessor, OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private datePickerDialog: DatePickerDialogService,
    private cd: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
  }

  @Input()
  placeholder = "";

  @Input()
  enableClear: boolean = false;

  @Input()
  format: string = "dd MMM yyyy";

  public onBlur(): void {
    this.onTouched();
  }

  value: string | null = null;

  get formattedValue() {
    var day = Day.parse(this.value);
    if (day == null)
      return null;
    return day.format(this.format);
  }

  // Client Events

  clear($event: Event) {
    this.value = null;
    this.onChange(null);
    this.cd.detectChanges();
  }

  togglePopup($event: Event) {
    this.datePickerDialog.open({ value: this.value }).subscribe((result) => {
      this.value = result;
      this.onChange(result);
      this.cd.detectChanges();
    });
  }

  // ControlValueAccessor API

  doChangeCallback() {
    this.onChange(this.value)
  }

  onChange: (value: string | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string | null): void {
    this.value = value;
    this.cd.detectChanges();
  }

  public registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    this._isDisabled = isDisabled;
  }
  private _isDisabled = false;
}
