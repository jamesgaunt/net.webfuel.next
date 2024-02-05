import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, ElementRef, forwardRef, HostBinding, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { Day } from '../date-calendar/Day';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

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

  popupFormControl: FormControl = new FormControl<string | null>(null);

  constructor(
    private overlay: Overlay,
    private viewContainerRef: ViewContainerRef,
    private cd: ChangeDetectorRef
  ) {
    this.popupFormControl.valueChanges
      .pipe(
        tap(value => {
          this.value = value;
          this.onChange(value);
          this.cd.detectChanges();
          this.closePopup();
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  @HostBinding('class.control-host') host = true;

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
    if (this._isDisabled)
      return;

    this.value = null;
    this.onChange(null);
    this.cd.detectChanges();
  }

  togglePopup($event: Event) {
    if (this._isDisabled)
      return;

    this.popupOpen ? this.closePopup() : this.openPopup();
  }

  // Popup

  @ViewChild('popupTemplate', { static: false })
  private popupTemplate!: TemplateRef<any>;

  @ViewChild('popupAnchor', { static: false })
  private popupAnchor!: ElementRef<any>;

  popupRef: OverlayRef | null = null;

  get popupOpen() {
    return this.popupRef !== null;
  }

  openPopup() {
    if (this.popupRef)
      return;

    this.popupRef = this.overlay.create({
      scrollStrategy: this.overlay.scrollStrategies.reposition({
        scrollThrottle: 50
      }),
      positionStrategy: this.overlay.position().flexibleConnectedTo(this.popupAnchor).withPositions([
        { originX: 'start', originY: 'bottom', overlayX: 'start', overlayY: 'top' },
        { originX: 'start', originY: 'top', overlayX: 'start', overlayY: 'bottom' },
        { originX: 'end', originY: 'bottom', overlayX: 'end', overlayY: 'top' },
        { originX: 'end', originY: 'top', overlayX: 'end', overlayY: 'bottom' },
      ]).withFlexibleDimensions(true).withPush(false),
      hasBackdrop: true,
    });
    this.popupRef.backdropClick().subscribe(() => this.closePopup());
    const portal = new TemplatePortal(this.popupTemplate, this.viewContainerRef);
    this.popupRef.attach(portal);
  }

  closePopup() {
    if (!this.popupOpen)
      return;
    this.popupRef!.detach();
    this.popupRef = null;
    this.onTouched();
    this.cd.detectChanges();
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
    this.cd.detectChanges();
  }
  public _isDisabled = false;
}
