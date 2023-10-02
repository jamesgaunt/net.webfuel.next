import { EventEmitter, Output } from "@angular/core";
import { GridDataSource } from "./grid-data-source";
import _ from '../underscore';
import { Observable } from "rxjs";
import { Query, QueryFilter, QueryResult } from "../../api/api.types";

export interface ISelectDataSource<TItem> {
  getId(item: TItem): string;

  change: EventEmitter<any>;

  pickedItems: TItem[];

  optionItems: TItem[];

  clear(): void;

  pick(ids: string[], clear: boolean): void;

  remove(id: string): void;

  fetch(flush: boolean): void;

  optionItemsCallback: Object | null; // TODO: Expose this in a more meaningful way (to the template - e.g. loading)
}

export class SelectDataSource<TItem> implements ISelectDataSource<TItem>  {

  constructor(private options: {
    fetch: (query: Query) => Observable<QueryResult<TItem>>;
    items?: TItem[]
  }) {
    if (options.items) {
      this.optionItems = options.items;
      this.optionItemsComplete = true;
    }
  }

  id = "id";

  getId(item: TItem) {
    return (<any>item)[this.id]
  }

  // Events

  change = new EventEmitter<any>();

  // Option Items

  optionItems: TItem[] = [];

  optionItemsCallback: Object | null = null; // Tracks the current active callback for select items

  optionItemsComplete: boolean = false; // Indicates there are no more select items to be loaded

  fetch(flush: boolean) {
    if (flush) {
      this.optionItemsCallback = null;
      this.optionItemsComplete = false;
    }

    if (this.optionItemsComplete || this.optionItemsCallback != null)
      return; // We have loaded all items or there is another fetch in progress

    // Unique token to represent the current callback
    var currentCallback = this.optionItemsCallback = new Object();

    this.options.fetch({
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
      this.change.emit();
    })
  }

  // Picked Items

  public pickedItems: TItem[] = [];

  clear() {
    this.pickedItems = [];
    this.change.emit();
  }

  pick(ids: string[], clear: boolean) {
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
        this.push(items);
        this.change.emit();
        return;
      }
    }

    // Build a filter
    var filter: QueryFilter = { op: "or", filters: [] };
    _.forEach(ids, (id) => {
      filter.filters!.push({ field: this.id, op: "eq", value: id });
    });

    // We need to use the api
    this.options.fetch({
      projection: [],
      sort: [],
      skip: 0,
      take: 100,
      filters: [filter],
    }).subscribe((response) => {
      if (clear)
        this.pickedItems = [];
      this.push(response.items);
      this.change.emit();
    });
  }

  remove(id: string) {
    this.pickedItems = _.remove(this.pickedItems, p => this.getId(p) === id);
  }

  push(items: TItem[]) {
    _.forEach(items, (item) => {
      if (item && !_.some(this.pickedItems, p => this.getId(p) === this.getId(item)))
        this.pickedItems.push(item);
    });
  }
}
