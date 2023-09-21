import _ from '../underscore';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Query, QueryFilter, QueryResult, QuerySort } from '../../api/api.types';

export class GridDataSource<TItem>  {

  constructor(private options: {
    fetch: (query: Query) => Observable<QueryResult<TItem>>;
    localStorageKey?: string;
    filterGroup?: FormGroup;
  }) {
    if (options.filterGroup) {
      options.filterGroup.valueChanges
        .pipe(
          debounceTime(200),
          tap(value => this.fetch()),
        )
        .subscribe();
    }
  }

  query: Query = {
    skip: 0, take: 10, sort: [], filters: [], projection: [], search: ''
  };

  queryResult: QueryResult<TItem> = { totalCount: -1, items: [] }; // totalCount == -1 indicates no results yet returned

  change: BehaviorSubject<GridDataSource<TItem>> = new BehaviorSubject<GridDataSource<TItem>>(this);

  // Fetch

  fetch() {
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

  // Sort Helpers

  private sortDirection(field: string) {
    if (!this.query.sort || this.query.sort.length === 0)
      return 0;
    let result = 0;
    _.forEach(this.query.sort, (p) => {
      if (p.field === field)
        result = p.direction;
    });
    return result;
  }

  private sortBy(field: string, direction: number) {
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

  private sortToggle(field: string, reverse?: boolean) {
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

