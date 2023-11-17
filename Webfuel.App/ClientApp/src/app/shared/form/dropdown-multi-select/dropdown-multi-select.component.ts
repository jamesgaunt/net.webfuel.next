import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { noop } from 'rxjs';
import _ from 'shared/common/underscore';
import { DropDownBase } from '../../common/dropdown-base';
import { Query } from '../../../api/api.types';

@Component({
  selector: 'dropdown-multi-select',
  templateUrl: './dropdown-multi-select.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => DropDownMultiSelectComponent),
      multi: true
    }
  ]
})
export class DropDownMultiSelectComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor {

  // Client Events

  pickItem(item: TItem) {
    if (this._isDisabled)
      return;

    var id = this.getId(item);
    this.pickItems([id], false);
    this.closePopup();
    this.doChangeCallback();
    this.cd.detectChanges();
  }

  removeItem(item: any) {
    if (this._isDisabled)
      return;

    this.removePickedItem(this.getId(item));
    this.doChangeCallback();
  }

  doChangeCallback() {
    this.onChange(_.map(this.pickedItems, (p) => this.getId(p)));
    this.checkFreeText();
  }

  // ControlValueAccessor API

  onChange: (value: string[] | null) => void = noop;

  public writeValue(value: string[] | null): void {
    this.pickItems(!value ? [] : value, true);
  }

  public registerOnChange(fn: (value: string[] | null) => void): void {
    this.onChange = fn;
  }
}
