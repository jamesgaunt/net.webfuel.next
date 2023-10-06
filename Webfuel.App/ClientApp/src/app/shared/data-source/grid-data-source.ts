import _ from '../underscore';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Query, QueryFilter, QueryResult, QuerySort } from '../../api/api.types';
import { EventEmitter } from '@angular/core';

export interface IGridDataSource<TItem> {
  change: EventEmitter<any>;
  queryResult: QueryResult<TItem>;
  fetch(): void;
  reorderable: boolean;
  reorder(previousIndex: number, currentIndex: number): void;
}

export interface IGridDataSourceOptions<TItem> {
  fetch: (query: Query) => Observable<QueryResult<TItem>>;
  reorder?: (items: TItem[]) => Observable<TItem[]>;
  localStorageKey?: string;
  filterGroup?: FormGroup;
  maxTake?: number;
  mode?: 'grid' | 'table';
}

const MAX_TAKE = 50;
export class GridDataSource<TItem>  {

  constructor(private options: IGridDataSourceOptions<TItem>) {

    this.maxTake = this.options.maxTake || this.maxTake;

    this.reorderable = !!options.reorder;

    if (this.reorderable)
      this.mode = 'table';
    else
      this.mode = this.options.mode || this.mode;

    if (options.filterGroup) {
      options.filterGroup.valueChanges
        .pipe(
          debounceTime(200),
          tap(value => this.fetch()),
        )
        .subscribe();
    }
  }

  // Events

  change = new EventEmitter<any>();

  // Properties

  query: Query = {
    skip: 0, take: 10, sort: [], filters: [], projection: [], search: ''
  };

  queryResult: QueryResult<TItem> = { totalCount: -1, items: [] }; // totalCount == -1 indicates no results yet returned

  reorderable = false;

  maxTake = MAX_TAKE;

  mode: 'grid' | 'table' = 'grid';

  // Fetch

  fetch() {

    if (this.mode == 'table')
      this.query.take = MAX_TAKE;
    else if (this.query.take > this.maxTake)
      this.query.take = this.maxTake;

    //if (!this.localStorageLoaded) {
    //  this.loadLocalStorage();
    //}

    this.options.fetch(this.query).subscribe((response) => {
      if (response.totalCount > 0 && response.items.length === 0 && this.query.skip! > 0) {
        this.query.skip = 0;
        this.fetch();
        return;
      }
      this.queryResult = response;
      this.change.next(this);
    });
  }

  // Reorder



  reorder(previousIndex: number, currentIndex: number) {
    if (!this.options.reorder)
      return;

    // Client Side
    const item = this.queryResult.items.splice(previousIndex, 1);
    this.queryResult.items.splice(currentIndex, 0, item[0]);

    // Server Side
    this.options.reorder(this.queryResult.items).subscribe((response) => {
      this.queryResult.items = response;
      this.change.next(this);
    });
  }

  // Sort Helpers

  sortDirection(field: string) {
    if (!this.query.sort || this.query.sort.length === 0)
      return 0;
    let result = 0;
    _.forEach(this.query.sort, (p) => {
      if (p.field === field)
        result = p.direction;
    });
    return result;
  }

  sortBy(field: string, direction: number) {
    let done = false;
    if (!this.query.sort)
      this.query.sort = [];
    _.forEach(this.query.sort, (p) => {
      if (p.field === field) {
        p.direction = direction;
        done = true;
      }
    });
    if (!done)
      this.query.sort.push({ field: field, direction: direction });
    if (_.all(this.query.sort, (p) => p.direction === 0))
      this.query.sort = [];
  }

  sortToggle(field: string, reverse?: boolean) {
    let direction = this.sortDirection(field);
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
    this.sortBy(field, direction);
  }
}

