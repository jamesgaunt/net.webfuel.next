import { Observable, debounceTime, noop, tap } from 'rxjs';
import { Query, QueryFilter, QueryResult } from 'api/api.types';
import { ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, EventEmitter, HostBinding, HostListener, Input, Output, TemplateRef, ViewChild, ViewContainerRef, inject } from '@angular/core';
import _ from 'shared/common/underscore';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { FormControl, Validators } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { QueryOp } from 'api/api.enums';
import { QueryService } from 'core/query.service';

export interface IDropDownDataSource<TItem, TQuery extends Query = Query> {
  query: (query: TQuery) => Observable<QueryResult<TItem>>;
}

@Component({
  template: ''
})
export abstract class DropDownBase<TItem> {

  @HostBinding('class.control-host') host = true;

  destroyRef: DestroyRef = inject(DestroyRef);
  hostRef: ElementRef = inject(ElementRef);
  overlay: Overlay = inject(Overlay);
  viewContainerRef: ViewContainerRef = inject(ViewContainerRef);
  cd: ChangeDetectorRef = inject(ChangeDetectorRef);
  queryService: QueryService = inject(QueryService);

  ngOnInit(): void {
    this.searchControl.valueChanges
      .pipe(
        debounceTime(200),
        tap(value => this.onSearchControlChange(value)),
        takeUntilDestroyed(this.destroyRef),
      )
      .subscribe();
  }

  id = "id";

  name = "name";

  getId(item: TItem) {
    return (<any>item)[this.id]
  }

  formatItem(item: TItem) {
    return (<any>item)[this.name]
  }

  @Input()
  placeholder = "";

  @Input()
  filterHidden = true;

  @Input()
  enableClear: boolean = false;

  @Input()
  enableSearch: boolean = true;

  @Input()
  optionFilter: (item: TItem) => boolean = () => true;

  @Output()
  filter = new EventEmitter<Query>();

  // Data Source

  @Input()
  dataSource: IDropDownDataSource<TItem> | undefined;

  @Input()
  set items(value: TItem[]) {
    this.dataSource = {
      query: (query: Query) => this.queryService.query(query, value)
    };
  }

  // Callback Icon

  @Input()
  callbackIcon: string | null = null;

  @Output()
  callback = new EventEmitter<any>();

  onCallback() {
    this.callback.emit(null);
  }

  // Option Items

  optionItems: TItem[] = [];

  optionItemsCallback: Object | null = null; // Tracks the current active callback for select items

  optionItemsComplete: boolean = false; // Indicates there are no more select items to be loaded

  fetch(flush: boolean) {

    if (!this.dataSource)
      return;

    if (flush) {
      this.optionItemsCallback = null;
      this.optionItemsComplete = false;
    }

    if (this.optionItemsComplete || this.optionItemsCallback != null)
      return; // We have loaded all items or there is another fetch in progress

    // Unique token to represent the current callback
    var currentCallback = this.optionItemsCallback = new Object();

    var q: Query = {
      skip: flush ? 0 : this.optionItems.length,
      take: 20,
      filters: []
    };

    if (this.filterHidden) {
      q.filters!.push({ field: 'hidden', op: QueryOp.NotEqual, value: true });
    }

    if (this.enableSearch && this.searchControl.getRawValue())
      q.search = this.searchControl.getRawValue() || "";

    if (this.filter.observed)
      this.filter.emit(q);

    this.dataSource.query(q).subscribe({
      next: (response) => {
        if (currentCallback !== this.optionItemsCallback)
          return; // We have forced a reload while waiting for these items to come back
        this.optionItemsCallback = null;
        if (flush)
          this.optionItems = [];
        if (response.items.length == 0) {
          this.optionItemsComplete = true;
        } else {
          this.optionItems = this.optionItems.concat(response.items.filter(p => this.optionFilter(p)));
        }
        this.cd.detectChanges();
      },
      error: (error) => {
        console.log("Error fetching data in dropdown-base: " + error);
      }
    })
  }

  // Picked Items

  public pickedItems: TItem[] = [];

  clear() {
    if (this._isDisabled)
      return;

    this.clearPickedItems();
    this.closePopup();
    this.doChangeCallback();
  }

  clearPickedItems() {
    this.pickedItems = [];
    this.cd.detectChanges();
  }

  abstract pickItem(item: TItem): void;

  picked(item: TItem) {
    return _.some(this.pickedItems, (p) => this.getId(p) == this.getId(item));
  }

  pickItems(ids: string[], clear: boolean) {

    // Can we 'cheat' and load these items from the option items
    {
      var items: any[] = [];
      _.forEach(ids, (id) => {
        var item = _.find(this.optionItems, (p) => this.getId(p) === id)
        if (item)
          items.push(item);
      });
      if (items.length == ids.length) {
        if (clear)
          this.pickedItems = [];
        this.pushPickedItems(items);
        return;
      }
    }

    // Build a filter
    var filter: QueryFilter = { op: "or", filters: [] };
    _.forEach(ids, (id) => {
      filter.filters!.push({ field: this.id, op: "eq", value: id });
    });

    // We need to use the api
    this.dataSource!.query({
      projection: [],
      sort: [],
      skip: 0,
      take: 100,
      filters: [filter],
    }).subscribe((response) => {
      if (clear)
        this.pickedItems = [];
      this.pushPickedItems(response.items);
    });
    this.cd.detectChanges();
  }

  removePickedItem(id: string) {
    this.pickedItems = _.remove(this.pickedItems, p => this.getId(p) === id);
    this.cd.detectChanges();
  }

  pushPickedItems(items: TItem[]) {
    _.forEach(items, (item) => {
      if (item && !_.some(this.pickedItems, p => this.getId(p) === this.getId(item)))
        this.pickedItems.push(item);
    });
    this.cd.detectChanges();
  }

  // Free Text

  @Input()
  freeTextControl: FormControl | null = null;

  @Input()
  freeTextPlaceholder: string | null = null;

  get pickedFreeText() {
    var picked = _.some(this.pickedItems, (p: any) => p.freeText);
    if (this.freeTextControl) {
      if (picked)
        this.freeTextControl.setValidators([Validators.required]);
      else
        this.freeTextControl.setValidators([]);
    }
    return picked;
  }

  checkFreeText() {
    if (this.freeTextControl != null && !this.pickedFreeText)
      this.freeTextControl.setValue("");
  }

  // Popup

  @ViewChild('popupTemplate', { static: false })
  private popupTemplate!: TemplateRef<any>;

  @ViewChild('popupAnchor', { static: false })
  private popupAnchor!: ElementRef<any>;

  popupRef: OverlayRef | null = null;

  // Active Index

  activeIndex: number | null = null;

  setActiveIndex(index: number | null) {
    if (this.activeIndex == index)
      return;

    if (this.optionItems.length == 0 || index == null)
      index = null;
    else if (index < 0)
      index = 0;
    else if (index > this.optionItems.length)
      index = this.optionItems.length;

    this.activeIndex = index;
    this.cd.detectChanges();
    this.scrollActiveIntoView();
  }

  scrollActiveIntoView() {
    setTimeout(() => {
      if (this.activeIndex == null)
        return;
      var active = document.querySelector('.dropdown-popup .active');
      if (active)
        active.scrollIntoView({ block: "nearest" });
    });
  }

  get popupOpen() {
    return this.popupRef !== null;
  }

  openPopup() {
    if (this._isDisabled)
      return;

    setTimeout(() => {
      if (this.searchInput && this.searchInput.nativeElement)
        this.searchInput.nativeElement.focus();
    }, 100);

    if (this.popupRef)
      return;

    this.setActiveIndex(null);

    if (this.enableSearch)
      this.focusControl.setValue('');

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
    });
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
    this.onTouched();
  }

  togglePopup() {
    this.popupOpen ? this.closePopup() : this.openPopup();
  }

  @HostListener('window:resize')
  private syncPopupWidth() {
    if (!this.popupOpen)
      return;
    const refRect = this.popupAnchor!.nativeElement.getBoundingClientRect();
    this.popupRef!.updateSize({ width: refRect.width });
  }

  @HostListener('document:click', ['$event'])
  clickout($event: MouseEvent) {
    if (!$event.target)
      return;

    if (!this.popupOpen || this.hostRef.nativeElement.contains($event.target))
      return;
    if (this.popupRef && this.popupRef.hostElement && this.popupRef.hostElement.contains(<any>$event.target))
      return;
    this.closePopup();
  }

  // Focus Input

  @ViewChild('FocusInput', { static: false })
  protected focusInput!: ElementRef<any>;

  focusControl = new FormControl<string>('');

  focusKeyUp($event: KeyboardEvent) {
    if ($event.key == 'Enter' || $event.key == 'ArrowDown') {
      if (!this.popupOpen) {
        this.openPopup();
        return;
      }
    }
  }

  focusKeyPress($event: KeyboardEvent) {
    if ($event.key == "Enter") {
      $event.stopPropagation();
      $event.preventDefault();
    }
  }

  // Search Input

  @ViewChild('SearchInput', { static: false })
  private searchInput!: ElementRef<any>;

  searchControl = new FormControl<string>('');

  onSearchControlChange(value: string | null) {
    if (this.enableSearch)
      this.fetch(true);
  }

  searchKeyUp($event: KeyboardEvent) {

    $event.preventDefault();
    $event.stopPropagation();

    /*
    if (($event.key == "Backspace" || $event.key == "Delete") && this.enableClear) {
      this.clearPickedItems();
      this.doChangeCallback();
      this.closePopup();
      return;
    }
    */

    if ($event.key == "Escape") {
      this.closePopup();
      this.focusInput.nativeElement.focus();
      return;
    }

    if ($event.key == 'Enter') {
      if (this.activeIndex !== null) {
        this.pickItem(this.optionItems[this.activeIndex]);
      }
      else {
        this.closePopup();
      }
      return;
    }

    if ($event.key == 'ArrowDown') {
      if (this.activeIndex == null)
        this.setActiveIndex(0);
      else
        this.setActiveIndex(this.activeIndex + 1);
      return;
    }

    if ($event.key == 'ArrowUp') {
      if (this.activeIndex != null) {
        this.setActiveIndex(this.activeIndex - 1);
        if (this.activeIndex < 0)
          this.setActiveIndex(0);
      }
      return;
    }
  }

  searchKeyDown($event: KeyboardEvent) {
    if ($event.key == "Tab") {
      this.closePopup();
      this.focusInput.nativeElement.focus();
    }
  }

  searchKeyPress($event: KeyboardEvent) {
    if ($event.key == "Enter") {
      $event.stopPropagation();
      $event.preventDefault();
    }
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

  // Misc

  abstract doChangeCallback(): void;

  // ControlValueAccessor API

  registerOnTouched(fn: () => void): void {
    this.onTouched = fn;
  }
  onTouched: () => void = noop;

  setDisabledState?(isDisabled: boolean): void {
    this._isDisabled = isDisabled;
    this.cd.detectChanges();
  }
  public _isDisabled = false;
}
