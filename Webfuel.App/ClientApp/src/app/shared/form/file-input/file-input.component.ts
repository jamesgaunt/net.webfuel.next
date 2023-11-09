import { ChangeDetectionStrategy, Component, DestroyRef, forwardRef, HostListener, inject, Input, OnInit } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'file-input',
  templateUrl: './file-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileInputComponent),
      multi: true
    }
  ]
})
export class FileInputComponent implements ControlValueAccessor, OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
  ) {
  }

  ngOnInit(): void {
  }

  // Inputs

  @Input()
  placeholder = "";

  public onBlur(): void {
    this.onTouched();
  }

  // Files

  filesAdded(event: any) {
    if (event && event.target && event.target.files && event.target.files.length) {
      for (var i = 0; i < event.target.files.length; i++) {
        console.log(event.target.files[i]);
        this.files.push(event.target.files[i]);
      }
    }
    this.onChange(this.files);
  }

  files: File[] = [];

  // ControlValueAccessor API

  onChange: (value: File[]) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: File[]): void {
    // Not Applicable
  }

  public registerOnChange(fn: (value: File[]) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    // Not Applicable
  }
}
