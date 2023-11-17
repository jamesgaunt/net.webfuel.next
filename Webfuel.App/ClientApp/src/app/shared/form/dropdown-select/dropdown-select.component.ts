import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { DropDownBase } from '../../common/dropdown-base';

@Component({
  selector: 'dropdown-select',
  templateUrl: './dropdown-select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownSelectComponent),
      multi: true
    }
  ]
})
export class DropDownSelectComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor {

  // Client Events

  pickItem(item: TItem) {
    if (this._isDisabled)
      return;

    var id = this.getId(item);
    if (this.pickedItems.length == 1 && id == this.getId(this.pickedItems[0])) {
      this.closePopup();
      return; // No change
    }

    this.clearPickedItems();
    this.pickItems([id], false);
    this.closePopup();
    this.doChangeCallback();
    this.cd.detectChanges();
  }

  doChangeCallback() {
    if (this.pickedItems.length === 0)
      this.onChange(null);
    else
      this.onChange(this.getId(this.pickedItems[0]));
    this.checkFreeText();
  }

  // ControlValueAccessor API

  onChange: (value: string | null) => void = noop;

  public writeValue(value: string | null): void {
    this.pickItems(!value ? [] : [value], true);
  }

  public registerOnChange(fn: (value: string | null) => void): void {
    this.onChange = fn;
  }
}
