import { ChangeDetectionStrategy, Component, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, ViewChild } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';

@Component({
  selector: 'file-input',
  templateUrl: './file-input.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => FileInputComponent),
      multi: true
    }
  ]
})
export class FileInputComponent implements ControlValueAccessor {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
  ) {
  }

  errorMessage = "";

  // Inputs

  @Input()
  placeholder = "";

  @Input()
  progress: number | null = null;

  @Input()
  showFiles = true;

  @Input()
  maxBytesTotal = 1024 * 1024 * 50;

  // Files

  filesAdded(event: any) {
    this.errorMessage = "";
    if (event && event.target && event.target.files && event.target.files.length) {
      for (var i = 0; i < event.target.files.length; i++) {
        this.pushFile(event.target.files[i]);
      }
    }
    this.onChange(this.files);
  }

  removeFile(file: File, $event: MouseEvent) {
    this.errorMessage = "";
    $event.preventDefault();
    $event.stopPropagation();

    var index = this.files.indexOf(file);
    if (index !== -1) {
      this.files.splice(index, 1);
      this.onChange(this.files);
    }
  }

  files: File[] = [];

  formatSize(size: number) {
    return _.formatBytes(size);
  }

  pushFile(file: File) {
    var total = 0;
    var duplicate = false;

    _.forEach(this.files, (f) => {
      total += f.size;
      if (f.name == file.name)
        duplicate = true;
    });

    if (duplicate)
      return;

    total += file.size;
    if (total > this.maxBytesTotal) {
      this.errorMessage = file.name + " not added. Maximum upload exceeded.";
      return;
    }
    this.files.push(file);
  }

  // Drop

  dragOver: boolean = false;

  onDrop($event: DragEvent) {
    this.errorMessage = "";
    this.dragOver = false;
    $event.preventDefault();
    $event.stopPropagation();

    if (!$event.dataTransfer || $event.dataTransfer.files.length == 0)
      return;

    for (var i = 0; i < $event.dataTransfer.files.length; i++) {
      this.pushFile($event.dataTransfer.files[i]);
    }
    this.onChange(this.files);
  }

  onDragEnter($event: DragEvent) {
    this.dragOver = true;
    $event.preventDefault();
    $event.stopPropagation();
  }

  onDragLeave($event: DragEvent) {
    this.dragOver = false;
    $event.preventDefault();
    $event.stopPropagation();
  }

  onDragOver($event: DragEvent) {
    this.dragOver = true;
    $event.preventDefault();
    $event.stopPropagation();
  }

  // ControlValueAccessor API

  onChange: (value: File[]) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: File[]): void {
    this.files = value || [];
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
