import _ from '../underscore';
import { BehaviorSubject, Observable, Observer } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { debounceTime, noop, tap } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { Query, QueryFilter, QueryResult, QuerySort } from '../../api/api.types';
import { EventEmitter } from '@angular/core';

// Data Source

export interface IDataSource<TItem> {
  fetch: (query: Query) => Observable<QueryResult<TItem>>;
}


export class DataSource<TItem>  {



  data: IDataSource<TItem> = {
    fetch: (query) => new Observable<QueryResult<TItem>>()
  }

  
}

