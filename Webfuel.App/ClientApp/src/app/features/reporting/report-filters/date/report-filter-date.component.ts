import { ChangeDetectionStrategy, ChangeDetectorRef, Component, DestroyRef, ElementRef, forwardRef, inject, Input, OnInit, TemplateRef, ViewChild, ViewContainerRef } from '@angular/core';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ControlValueAccessor, FormControl, FormGroup, NG_VALUE_ACCESSOR, Validators } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { ReportFilterDate, ReportFilterDateCondition } from '../../../../api/api.types';
import _ from 'shared/common/underscore';
import { TemplatePortal } from '@angular/cdk/portal';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { Day } from '../../../../shared/form/date-calendar/Day';

@Component({
  selector: 'report-filter-date',
  templateUrl: './report-filter-date.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ReportFilterDateComponent),
      multi: true
    }
  ]
})
export class ReportFilterDateComponent implements ControlValueAccessor, OnInit {

  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  destroyRef: DestroyRef = inject(DestroyRef);

  popupFormControl: FormControl = new FormControl<string | null>(null);

  constructor(
    private overlay: Overlay,
    private viewContainerRef: ViewContainerRef,
  ) {
    this.popupFormControl.valueChanges
      .pipe(
        tap(value => {
          this.form.patchValue({ value: this.formatValue(value) });
          this.closePopup();
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  formatValue(value: string) {
    var day = Day.parse(value);
    if (day == null)
      return null;
    return day.format("dd MMM yyyy");
  }

  ngOnInit(): void {
    this.form.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => {
          this.filter = _.merge(this.filter, value);
          this.emitChanges()
        }),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  reset(filter: ReportFilterDate) {
    this.filter = filter;
    this.form.patchValue(filter);
  }

  filter!: ReportFilterDate;

  form = new FormGroup({
    name: new FormControl<string>('', { nonNullable: true }),
    editable: new FormControl<boolean>(false, { nonNullable: true }),
    condition: new FormControl<ReportFilterDateCondition>(ReportFilterDateCondition.EqualTo),
    value: new FormControl<string>(''),
  });

  get unary() {
    var condition = this.filter.conditions.find(c => c.value == this.form.value.condition);
    if (condition == undefined)
      return false;
    return condition.unary;
  }

  // Drop Down Options

  options: { id: string, name: string }[] = [
    { id: 'today', name: 'today' },
    { id: 'yesterday', name: 'yesterday' },
    { id: 'start of month', name: 'start of month' },
    { id: 'end of month', name: 'end of month' },
    { id: 'start of year', name: 'start of year' },
    { id: 'end of year', name: 'end of year' },
  ];

  onCallback() {
    this.openPopup();
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

  // Inputs

  // ControlValueAccessor API

  emitChanges() {
    this.cd.detectChanges();
    this.onChange(_.deepClone(this.filter));
  }

  onChange: (value: ReportFilterDate) => void = noop;
  onTouched: () => void = noop;

  public writeValue(value: ReportFilterDate): void {
    this.reset(_.deepClone(value));
  }

  public registerOnChange(fn: (value: ReportFilterDate) => void): void {
    this.onChange = fn;
  }

  public registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }

  public setDisabledState?(isDisabled: boolean): void {
    isDisabled ? this.form.disable() : this.form.enable();
  }
}
