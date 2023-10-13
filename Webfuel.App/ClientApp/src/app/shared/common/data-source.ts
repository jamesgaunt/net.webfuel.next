import { Observable } from 'rxjs';
import { Query, QueryResult } from '../../api/api.types';
import { EventEmitter } from '@angular/core';

export interface IDataSource<TItem> {

  fetch: (query: Query) => Observable<QueryResult<TItem>>;

  changed?: EventEmitter<any>;
}

