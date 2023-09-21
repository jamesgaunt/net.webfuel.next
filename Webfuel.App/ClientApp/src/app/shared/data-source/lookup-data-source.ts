import { BehaviorSubject, Observable } from "rxjs";
import _ from '../underscore';
import { Query, QueryResult } from "../../api/api.types";

export class LookupDataSource<TItem>  {

  constructor(private options: {
    fetch: (query: Query) => Observable<QueryResult<TItem>>;
  }) {
  }

  cache: { [key: string]: BehaviorSubject<TItem | null> } = {};

  lookup(id: string): BehaviorSubject<TItem | null> {

    if (this.cache[id] == undefined) {
      this.cache[id] = new BehaviorSubject<TItem | null>(null);

      this.options.fetch({
        skip: 0, take: 1, filters: [
          { field: "id", op: "eq", value: id }
        ]
      }).subscribe((result) => {
        if (result.items.length == 1)
          this.cache[id].next(result.items[0]);
      });
    }
    return this.cache[id];
  }
}
