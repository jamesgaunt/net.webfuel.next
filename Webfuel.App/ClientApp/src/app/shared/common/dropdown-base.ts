import { Observable } from 'rxjs';
import { Query, QueryFilter, QueryResult } from '../../api/api.types';
import { ChangeDetectorRef, Component, ContentChild, DestroyRef, ElementRef, EventEmitter, HostListener, Input, Output, TemplateRef, ViewChild, ViewContainerRef, inject } from '@angular/core';
import { IDataSource } from './data-source';
import _ from 'shared/common/underscore';
import { Overlay, OverlayRef } from '@angular/cdk/overlay';
import { TemplatePortal } from '@angular/cdk/portal';
import { QueryOp } from '../../api/api.enums';

@Component({
  template: ''
})
export class DropDownBase<TItem> {

  destroyRef: DestroyRef = inject(DestroyRef);
  overlay: Overlay = inject(Overlay);
  viewContainerRef: ViewContainerRef = inject(ViewContainerRef);
  cd: ChangeDetectorRef = inject(ChangeDetectorRef);

  id = "id";

  getId(item: TItem) {
    return (<any>item)[this.id]
  }

  @Input()
  placeholder = "";

  @Input()
  filterHidden = false;

  @Output()
  filter = new EventEmitter<Query>();

  // Data Source

  @Input({ required: true })
  dataSource: IDataSource<TItem> | undefined;

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

    if (this.filter.observed)
      this.filter.emit(q);

    this.dataSource.query(q).subscribe((response) => {
      if (currentCallback !== this.optionItemsCallback)
        return; // We have forced a reload while waiting for these items to come back
      this.optionItemsCallback = null;
      if (flush)
        this.optionItems = [];
      if (response.items.length == 0) {
        this.optionItemsComplete = true;
      } else {
        this.optionItems = this.optionItems.concat(response.items);
      }
      this.cd.detectChanges();
    })
  }

  // Picked Items

  public pickedItems: TItem[] = [];

  clearPickedItems() {
    this.pickedItems = [];
    this.cd.detectChanges();
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
}
