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
export class DropDownMultiSelectComponent<TItem>
  extends DropDownBase<TItem> implements ControlValueAccessor, OnInit {

  ngOnInit(): void {
  }

  @Input()
  enableClear: boolean = false;

  @Input()
  enableSearch: boolean = true;

  // Client Events

  pickItem(item: TItem) {
    if (this._isDisabled)
      return;

    var id = this.getId(item);
    this.pickItems([id], false);
    this.closePopup();
    this.doChangeCallback();
  }

  removeItem(item: any, $event: Event) {
    if ($event) {
      $event.preventDefault();
      $event.stopPropagation();
    }
    if (this._isDisabled)
      return;

    this.removePickedItem(this.getId(item));
    this.doChangeCallback();
  }

  clear($event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
    if (this._isDisabled)
      return;

    this.clearPickedItems();
    this.closePopup();
    this.doChangeCallback();
  }

  togglePopup($event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
    if (this._isDisabled)
      return;

    this.popupOpen ? this.closePopup() : this.openPopup();
  }

  // ControlValueAccessor API

  doChangeCallback() {
    if (this.pickedItems.length === 0)
      this.onChange(null);
    this.onChange(_.map(this.pickedItems, (p) => this.getId(p)));
  }

  onChange: (value: string[] | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string[] | null): void {
    this.pickItems(!value ? [] : value, true);
  }

  public registerOnChange(fn: (value: string[] | null) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    this._isDisabled = isDisabled;
    this.cd.detectChanges();
  }
  public _isDisabled = false;
}
