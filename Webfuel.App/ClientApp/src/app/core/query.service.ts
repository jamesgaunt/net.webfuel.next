import { Injectable } from "@angular/core";
import { Query, QueryFilter, QueryResult, QuerySort } from "../api/api.types";
import { QueryOp } from "../api/api.enums";
import { Observable } from "rxjs";
import _ from 'shared/common/underscore';
import { IDataSource } from "../shared/common/data-source";

@Injectable()
export class QueryService {
  constructor(
  ) {
  }

  // Sort Helpers

  sortDirection(query: Query, field: string) {
    if (!query.sort || query.sort.length === 0)
      return 0;
    let result = 0;
    _.forEach(query.sort, (p) => {
      if (p.field === field)
        result = p.direction;
    });
    return result;
  }

  sortBy(query: Query, field: string, direction: number) {
    let done = false;
    if (!query.sort)
      query.sort = [];
    _.forEach(query.sort, (p) => {
      if (p.field === field) {
        p.direction = direction;
        done = true;
      }
    });
    if (!done)
      query.sort.push({ field: field, direction: direction });
    if (_.all(query.sort, (p) => p.direction === 0))
      query.sort = [];
  }

  sortToggle(query: Query, field: string, reverse?: boolean) {
    let direction = this.sortDirection(query, field);
    if (reverse === true) {
      if (direction < 0)
        direction = 1;
      else if (direction > 0)
        direction = 0;
      else
        direction = -1;
    } else {
      if (direction > 0)
        direction = -1;
      else if (direction < 0)
        direction = 0;
      else
        direction = 1;
    }
    this.sortBy(query, field, direction);
  }

  // In-memory fetch

  query<TItem>(query: Query, items: TItem[]): Observable<QueryResult<TItem>> {

    var result = items;

    result = this._applyQueryFiltersToItemArray(query.filters, result);

    result = this._applySearchToItemArray(query.search, result);

    result = this._applyQuerySortToItemArray(query.sort, result);

    var totalCount = result.length;

    result = this._applyPageToItemArray(query.skip, query.take, result);

    return new Observable<QueryResult<TItem>>((subscriber) => {
      subscriber.next({ items: result, totalCount: totalCount });
      subscriber.complete();
    });
  }

  get<TItem>(id: string, items: TItem[]): Observable<TItem | null> {
    return new Observable<TItem  | null>((subscriber) => {
      subscriber.next(_.find(items, (p) => (<any>p)['id'] == id) || null);
      subscriber.complete();
    });
  }

  // Implementation

  private _applyPageToItemArray<TItem>(skip: number, take: number, items: TItem[]): TItem[] {

    var result: TItem[] = [];

    if (take <= 0)
      take = 100000;

    for (var i = skip; i < skip + take && i < items.length; i++)
      result.push(items[i]);

    return result;
  }

  private _applyQuerySortToItemArray<TItem>(sort: QuerySort[] | undefined, items: TItem[]): TItem[] {

    if (!sort)
      return items;

    var result: TItem[] = [...items];

    for (var i = 0; i < sort.length; i++) 
      result.sort((a, b) => this._applyQuerySortToItemPair(sort[i], a, b));

    return result;
  }

  private _applyQuerySortToItemPair(sort: QuerySort, item1: any, item2: any): number {

    if (!sort.field || !sort.direction)
      return 0;

    var value1 = item1[sort.field];
    var value2 = item2[sort.field];

    if (sort.direction > 0)
      return value1 > value2 ? 1 : -1;

    if (sort.direction < 0)
      return value1 > value2 ? -1 : 1;

    return 0;
  }

  private _applySearchToItemArray<TItem>(search: string | undefined, items: TItem[]): TItem[] {

    if (!search)
      return items;

    var result: TItem[] = [];

    for (var i = 0; i < items.length; i++) {
      if (this._applySearchToItem(search, items[i]))
        result.push(items[i]);
    }

    return result;
  }

  private _applySearchToItem(search: string | undefined, item: any): boolean {

    if (!search)
      return true;

    search = search.toLowerCase();
    for (const prop in item) {
      if (("" + item[prop]).toLowerCase().indexOf(search) >= 0)
        return true;
    }
    return false;
  }

  private _applyQueryFiltersToItemArray<TItem>(filters: QueryFilter[] | undefined, items: TItem[]): TItem[] {
    if (!filters)
      return items;
    return this._applyQueryFilterToItemArray({ op: QueryOp.And, filters: filters }, items);
  }

  private _applyQueryFilterToItemArray<TItem>(filter: QueryFilter, items: TItem[]): TItem[] {

    var result: TItem[] = [];

    for (var i = 0; i < items.length; i++) {
      if (this._applyQueryFilterToItem(filter, items[i]))
        result.push(items[i]);
    }

    return result;
  }

  private _applyQueryFilterToItem(filter: QueryFilter, item: any): boolean {

    if (filter.field)
      return this._applyQueryOpToField(item[filter.field], filter.op, filter.value);

    if (!filter.filters || filter.filters.length == 0)
      return true;

    if (filter.op == QueryOp.And) {
      for (var i = 0; i < filter.filters.length; i++) {
        if (this._applyQueryFilterToItem(filter.filters[i], item) === false)
          return false;
      }
      return true;
    }

    if (filter.op == QueryOp.Or) {
      for (var i = 0; i < filter.filters.length; i++) {
        if (this._applyQueryFilterToItem(filter.filters[i], item) === true)
          return true;
      }
      return false;
    }

    console.error(`QueryOp ${filter.op} is not implemented`);
    return false;
  }

  private _applyQueryOpToField(field: any, op: string, value: any) {
    switch (op) {
      case QueryOp.None:
        return true;

      case QueryOp.Equal:
        return field === value;

      case QueryOp.NotEqual:
        return field !== value;

      case QueryOp.GreaterThan:
        return field > value;

      case QueryOp.LessThan:
        return field < value;

      case QueryOp.GreaterThanOrEqual:
        return field >= value;

      case QueryOp.LessThanOrEqual:
        return field <= value;

      case QueryOp.Contains:
        return ("" + field).indexOf("" + value) >= 0;
    }

    console.error(`QueryOp ${op} is not implemented`);
    return false;
  }
}

