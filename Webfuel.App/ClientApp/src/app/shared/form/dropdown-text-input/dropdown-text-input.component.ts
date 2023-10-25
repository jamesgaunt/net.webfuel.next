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
export class DropDownTextInputComponent<TItem>
  extends DropDownBase<TItem> implements ControlValueAccessor, OnInit {

  formControl: FormControl = new FormControl<string>('');

  ngOnInit(): void {
    this.formControl.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => this.onChange(value)),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  @Input()
  enableClear: boolean = false;

  // Client Events

  pickItem(item: TItem, $event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
    this.closePopup();
    this.formControl.setValue((<any>item).name);
    this.cd.detectChanges();
  }

  clear($event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
    this.closePopup();
    this.formControl.setValue('');
    this.cd.detectChanges();
  }

  togglePopup($event: Event, $textInput: HTMLElement) {
    $event.preventDefault();
    $event.stopPropagation();
    if (this.popupOpen) {
      this.closePopup()
    }
    else {
      this.openPopup();
      $textInput.focus();
    }
  }

  // ControlValueAccessor API

  onChange: (value: string | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string | null): void {
    this.formControl.setValue(value, { emitEvent: false });
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
