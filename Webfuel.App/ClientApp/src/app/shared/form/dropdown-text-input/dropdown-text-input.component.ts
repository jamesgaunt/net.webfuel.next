import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import _ from 'shared/common/underscore';
import { DropDownBase } from '../../common/dropdown-base';

@Component({
  selector: 'dropdown-text-input',
  templateUrl: './dropdown-text-input.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownTextInputComponent),
      multi: true
    }
  ]
})
export class DropDownTextInputComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor {

  constructor() {
    super();
    this.enableSearch = false;
  }

  // Client Events

  pickItem(item: TItem) {
    if (this._isDisabled)
      return;

    this.closePopup();
    this.focusControl.setValue((<any>item).name);
    this.cd.detectChanges();
  }

  clear() {
    if (this._isDisabled)
      return;

    this.closePopup();
    this.focusControl.setValue('');
    this.cd.detectChanges();
  }

  doChangeCallback() {
  }

  onFocusControlChange(value: string | null) {
    this.onChange(value);
  }

  // ControlValueAccessor API

  public writeValue(value: string | null): void {
    this.focusControl.setValue(value, { emitEvent: false });
  }

  public registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }
  onChange: (value: string | null) => void = noop;
}
