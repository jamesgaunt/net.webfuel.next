import _ from './underscore';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { IQuery, IQueryResult } from '../api/api.types';
import { FormGroup } from '@angular/forms';

export class DataSource<TItem, TFilter> {

  constructor(private options: {
    fetch: (query: IQuery<TFilter>) => Observable<IQueryResult<TItem>>;
    query?: IQuery<TFilter>;
    localStorageKey?: string;
    filterForm?: FormGroup;
  }) {
    if (options.query)
      this.query = options.query;
  }

  public query: IQuery<TFilter> = { skip: 0, take: 15, sort: [], filter: null };

  public queryResult: IQueryResult<TItem> = { totalCount: -1, items: [] }; // totalCount == -1 indicates no results yet returned

  public change: BehaviorSubject<DataSource<TItem, TFilter>> = new BehaviorSubject<DataSource<TItem, TFilter>>(this);

  // Fetch

  fetch() {
    //if (!this.localStorageLoaded) {
    //  this.loadLocalStorage();
    //}

    this.options.fetch(this.query).subscribe((response) => {
      if (response.totalCount > 0 && response.items.length === 0 && this.query.skip > 0) {
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
    if (this.query.sort.length === 0)
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

