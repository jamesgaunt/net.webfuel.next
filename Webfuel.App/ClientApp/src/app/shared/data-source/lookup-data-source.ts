import { BehaviorSubject, Observable } from "rxjs";
import { IQuery, IQueryResult } from "../../api/api.types";
import _ from '../underscore';

export class LookupDataSource<TItem, TQuery extends IQuery>  {

  constructor(private options: {
    fetch: (query: IQuery) => Observable<IQueryResult<TItem>>;
  }) {
  }

  cache: { [key: string]: BehaviorSubject<TItem | null> } = {};

  lookup(id: string): BehaviorSubject<TItem | null> {

    if (this.cache[id] == undefined) {
      this.cache[id] = new BehaviorSubject<TItem | null>(null);

      this.options.fetch({
        skip: 0, take: 1, projection: [], sort: [], filters: [
          { field: "id", op: "eq", value: id, filters: [] }
        ]
      }).subscribe((result) => {
        if (result.items.length == 1)
          this.cache[id].next(result.items[0]);
      });
    }
    return this.cache[id];
  }
}
