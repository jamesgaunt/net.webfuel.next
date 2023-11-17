import { BehaviorSubject, Observable } from 'rxjs';
import { Query, QueryResult } from '../../api/api.types';
import { EventEmitter } from '@angular/core';

export interface IDataSource<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> {

  query: (query: TQuery) => Observable<QueryResult<TItem>>;

  get?: (params: { id: string }) => Observable<TItem | null>;

  create?: (command: TCreate) => Observable<TItem>;

  update?: (command: TUpdate) => Observable<TItem>;

  delete?: (command: { id: string }) => Observable<any>;

  sort?: (command: { ids: string[] }) => Observable<any>;

  changed?: EventEmitter<any>;
}

export interface IDataSourceWithGet<TItem, TQuery extends Query = Query, TCreate = any, TUpdate = any> extends IDataSource<TItem, TQuery, TCreate, TUpdate> {
  get: (params: { id: string }) => Observable<TItem | null>;
}

export class DataSourceLookup<TItem, TQuery extends Query = Query> implements IDataSource<TItem, TQuery> {

  constructor(dataSource: IDataSource<TItem>) {
    this.dataSource = dataSource;
  }

  dataSource: IDataSource<TItem>;

  query = (query: TQuery) => this.dataSource.query(query);

  get = (params: { id: string }) => {
    if (this._getCache[params.id] == undefined) {
      this._getCache[params.id] = new BehaviorSubject<TItem | null>(null);

      if (!this.dataSource)
        return new BehaviorSubject<TItem | null>(null);

      if (this.dataSource.get) {

        this.dataSource.get({ id: params.id })
          .subscribe((result) => {
            this._getCache[params.id].next(result);
          });
      }
      else {
        this.dataSource.query({
          skip: 0, take: 1, filters: [
            { field: "id", op: "eq", value: params.id }
          ]
        }).subscribe((result) => {
          if (result.items.length == 1)
            this._getCache[params.id].next(result.items[0]);
        });
      }
    }
    return this._getCache[params.id];
  }

  private _getCache: { [key: string]: BehaviorSubject<TItem | null> } = {};
}

