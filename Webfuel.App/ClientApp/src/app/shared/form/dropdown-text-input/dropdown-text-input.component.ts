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
export class DropDownTextInputComponent<TItem> extends DropDownBase<TItem> implements ControlValueAccessor, OnInit {

  formControl: FormControl = new FormControl<string>('');

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    overlay: Overlay,
    viewContainerRef: ViewContainerRef,
    cd: ChangeDetectorRef
  ) {
    super(overlay, viewContainerRef, cd);
  }

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

  public onBlur(): void {
    this.onTouched();
  }

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

  // Popup

  @ViewChild('popupTemplate', { static: false })
  private popupTemplate!: TemplateRef<any>;

  @ViewChild('popupAnchor', { static: false })
  private popupAnchor!: ElementRef<any>;

  popupRef: OverlayRef | null = null;

  get popupOpen() {
    return this.popupRef !== null;
  }

  // TODO: Force focus
  openPopup() {
    if (this.popupRef)
      return;

    this.popupRef = this.overlay.create({
      positionStrategy: this.overlay.position().flexibleConnectedTo(this.popupAnchor).withPositions([
        { originX: 'start', originY: 'bottom', overlayX: 'start', overlayY: 'top' },
        { originX: 'start', originY: 'top', overlayX: 'start', overlayY: 'bottom' },
        { originX: 'end', originY: 'bottom', overlayX: 'end', overlayY: 'top' },
        { originX: 'end', originY: 'top', overlayX: 'end', overlayY: 'bottom' },
      ]).withFlexibleDimensions(true).withPush(false),
      hasBackdrop: false,
    });
    this.popupRef.backdropClick().subscribe(() => this.closePopup());
    const portal = new TemplatePortal(this.popupTemplate, this.viewContainerRef);
    this.popupRef.attach(portal);
    this.syncPopupWidth();
    this.fetch(true);
  }

  closePopup() {
    if (!this.popupOpen)
      return;
    this.popupRef!.detach();
    this.popupRef = null;
  }

  delayedClosePopup() {
    setTimeout(() => this.closePopup(), 200);
  }

  @HostListener('window:resize')
  private syncPopupWidth() {
    if (!this.popupOpen)
      return;
    const refRect = this.popupAnchor!.nativeElement.getBoundingClientRect();
    this.popupRef!.updateSize({ width: refRect.width });
  }

  // Templates

  @ViewChild('defaultOptionTemplate', { static: false }) private defaultOptionTemplate!: TemplateRef<any>;
  @ContentChild('optionTemplate', { static: false }) private optionTemplate: TemplateRef<any> | undefined;

  @ViewChild('defaultPickedTemplate', { static: false }) private defaultPickedTemplate!: TemplateRef<any>;
  @ContentChild('pickedTemplate', { static: false }) private pickedTemplate: TemplateRef<any> | undefined;

  get _optionTemplate() {
    return this.optionTemplate || this.defaultOptionTemplate;
  }

  get _pickedTemplate() {
    return this.pickedTemplate || this.defaultPickedTemplate;
  }

  // Drop Down Scroll Handler

  scrollPosition: number = 0;
  scrollTrigger: number = 0.8;

  onDropDownScroll($event: Event) {

    var targetElement = <Element>$event.target;
    if (!targetElement)
      return;

    var n = targetElement.scrollTop;
    var d = targetElement.scrollHeight - targetElement.clientHeight;

    if (!_.isNumber(n) || !_.isNumber(d) || d <= 0)
      this.scrollPosition = 1;
    else
      this.scrollPosition = n / d;

    if (this.scrollPosition > 0.8)
      this.fetch(false);
  }

  // ControlValueAccessor API

  onChange: (value: string | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string | null): void {
    this.pickItems(!value ? [] : [value], true);
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
