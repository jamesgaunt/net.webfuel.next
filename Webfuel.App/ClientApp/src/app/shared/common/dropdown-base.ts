import { Observable } from 'rxjs';
import { Query, QueryFilter, QueryResult } from '../../api/api.types';
import { ChangeDetectorRef, Component, EventEmitter, Input, ViewContainerRef } from '@angular/core';
import { IDataSource } from './data-source';
import _ from 'shared/common/underscore';
import { Overlay } from '@angular/cdk/overlay';

@Component({
  template: ''
})
export class DropDownBase<TItem> {

  constructor(
    protected overlay: Overlay,
    protected viewContainerRef: ViewContainerRef,
    protected cd: ChangeDetectorRef
  ) {
  }

  id = "id";

  getId(item: TItem) {
    return (<any>item)[this.id]
  }

  @Input()
  placeholder = "";


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

    this.dataSource.fetch({
      skip: flush ? 0 : this.optionItems.length,
      take: 20,
    }).subscribe((response) => {
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

    if (!this.dataSource)
      return;

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
        this.cd.detectChanges();
        return;
      }
    }

    // Build a filter
    var filter: QueryFilter = { op: "or", filters: [] };
    _.forEach(ids, (id) => {
      filter.filters!.push({ field: this.id, op: "eq", value: id });
    });

    // We need to use the api
    this.dataSource.fetch({
      projection: [],
      sort: [],
      skip: 0,
      take: 100,
      filters: [filter],
    }).subscribe((response) => {
      if (clear)
        this.pickedItems = [];
      this.pushPickedItems(response.items);
      this.cd.detectChanges();
    });
  }

  removePickedItem(id: string) {
    this.pickedItems = _.remove(this.pickedItems, p => this.getId(p) === id);
  }

  pushPickedItems(items: TItem[]) {
    _.forEach(items, (item) => {
      if (item && !_.some(this.pickedItems, p => this.getId(p) === this.getId(item)))
        this.pickedItems.push(item);
    });
  }
}
