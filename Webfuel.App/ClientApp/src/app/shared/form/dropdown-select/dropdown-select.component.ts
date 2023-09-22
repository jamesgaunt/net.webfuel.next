import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, forwardRef, HostListener, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import _ from 'shared/underscore';
import { SelectDataSource } from '../../data-source/select-data-source';
import { GridDataSource } from '../../data-source/grid-data-source';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';

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
export class DropDownSelectComponent<TItem> implements ControlValueAccessor, OnInit {

  destroyRef: DestroyRef = inject(DestroyRef);

  constructor(
    private overlay: Overlay,
    private viewContainerRef: ViewContainerRef,
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

  // Data Source

  @Input({ required: true })
  set dataSource(value: SelectDataSource<TItem>) {
    this._dataSource = value;
    this._dataSource.change.subscribe(() => this.cd.detectChanges());
  }
  get dataSource() {
    return this._dataSource;
  }
  _dataSource!: SelectDataSource<TItem>

  getId(item: TItem) {
    return this.dataSource.getId(item);
  }

  // Client Events

  pickItem(item: TItem) {
    var id = this.getId(item);
    if (this.dataSource.pickedItems.length == 1 && id == this.getId(this.dataSource.pickedItems[0])) {
      this.closePopup();
      return; // No change
    }

    this.dataSource.clear();
    this.dataSource.pick([id], false);
    this.closePopup();
    this.doChangeCallback();
  }

  removeItem(item: any, $event: Event) {
    if ($event) {
      $event.preventDefault();
      $event.stopPropagation();
    }
    this.dataSource.remove((<any>item)[this.dataSource.id]);
    this.doChangeCallback();
  }

  clear($event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
    this.dataSource.clear();
    this.closePopup();
    this.doChangeCallback();
  }

  togglePopup($event: Event) {
    $event.preventDefault();
    $event.stopPropagation();
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
    this.syncPopupWidth();
    this.dataSource.fetch(true);
  }

  closePopup() {
    if (!this.popupOpen)
      return;
    this.popupRef!.detach();
    this.popupRef = null;
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
      this.dataSource.fetch(false);
  }

  // ControlValueAccessor API

  doChangeCallback() {
    if (this.dataSource.pickedItems.length === 0)
      this.onChange(null);
    this.onChange((<any>this.dataSource.pickedItems[0])[this.dataSource.id])
  }

  onChange: (value: string | null) => void = noop;

  onTouched: () => void = noop;

  public writeValue(value: string | null): void {
    this.dataSource.pick(!value ? [] : [value], true);
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
