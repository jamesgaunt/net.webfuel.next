import _ from '../underscore';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { IQuery, IQueryResult } from '../../api/api.types';

export class DataSource<TItem, TQuery extends IQuery> {

  constructor(private options: {
    fetch: (query: TQuery) => Observable<IQueryResult<TItem, TQuery>>;
    localStorageKey?: string;
  }) {

  }

  private query: TQuery | undefined;

  private queryResult: IQueryResult<TItem, TQuery> | undefined;




  // Sort Helpers

  /*
  sortDirection(field: string) {
    if (this.query.sort.length === 0)
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
  */
}

