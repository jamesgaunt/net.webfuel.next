import { Observable } from 'rxjs';
import { Query, QueryResult } from '../../api/api.types';

export interface IDataSource<TItem> {
  fetch: (query: Query) => Observable<QueryResult<TItem>>;
}

