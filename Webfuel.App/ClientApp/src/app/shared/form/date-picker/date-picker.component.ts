import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import _ from 'shared/underscore';
import { SelectDataSource } from '../../data-source/select-data-source';
import { GridDataSource } from '../../data-source/grid-data-source';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { Day } from '../date-calendar/Day';
import { DialogService } from '../../../core/dialog.service';

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
    private dialogService: DialogService,
    private cd: ChangeDetectorRef
  ) {
  }

  ngOnInit(): void {
  }

  @Input()
  placeholder = "";

  @Input()
  enableClear: boolean = false;

  public onBlur(): void {
    this.onTouched();
  }

  value: string | null = null;

  // Client Events

  togglePopup($event: Event) {
    this.dialogService.pickDate({
      callback: (result) => {
        this.value = result;
        this.onChange(result);
      }
    })
  }

  // ControlValueAccessor API

  doChangeCallback() {
    this.onChange(this.value)
  }

  onChange: (value: string | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string | null): void {
    this.value = value;
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
