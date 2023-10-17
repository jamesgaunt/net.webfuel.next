import { Observable } from 'rxjs';
import { Query, QueryResult } from '../../api/api.types';
import { EventEmitter } from '@angular/core';

export interface IDataSource<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> {

  query: (query: TQuery) => Observable<QueryResult<TItem>>;

  create?: (command: TCreate) => Observable<TItem>;

  update?: (command: TUpdate) => Observable<TItem>;

  delete?: (command: { id: string }) => Observable<any>;

  sort?: (command: { ids: string[] }) => Observable<any>;

  changed?: EventEmitter<any>;
}

