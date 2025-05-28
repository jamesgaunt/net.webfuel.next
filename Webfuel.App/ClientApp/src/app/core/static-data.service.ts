import { Injectable } from '@angular/core';
import { BehaviorSubject, first, switchMap } from 'rxjs';
import { IStaticDataModel, Query } from '../api/api.types';
import { StaticDataApi } from '../api/static-data.api';
import { QueryService } from './query.service';

@Injectable()
export class StaticDataService {
  constructor(private staticDataApi: StaticDataApi, private queryService: QueryService) {
    this._loadStaticData();
  }

  staticData = new BehaviorSubject<IStaticDataModel | null>(null);

  queryFactory<TItem>(query: Query, selector: (staticData: IStaticDataModel) => TItem[]) {
    return this.staticData.pipe(
      first((p) => p !== null), // This is a one-shot subscription
      switchMap((p) => this.queryService.query(query, selector(p!)))
    );
  }

  getFactory<TItem>(id: string, selector: (staticData: IStaticDataModel) => TItem[]) {
    return this.staticData.pipe(
      first((p) => p !== null), // This is a one-shot subscription
      switchMap((p) => this.queryService.get(id, selector(p!)))
    );
  }

  // Implementation

  private _loadingStaticData = false;

  private _loadStaticData() {
    console.log('Loading Static Data');

    if (this._loadingStaticData) {
      console.log('Static Data Already Loading');
      return;
    }
    this._loadingStaticData = true;

    this.staticDataApi.getStaticData().subscribe({
      next: (staticData) => {
        this._loadingStaticData = false;
        this.staticData.next(staticData);
        console.log('Loaded Static Data: ', { staticData });
      },
      error: (err) => {
        this._loadingStaticData = false;
        console.log('Error Loading Static Data: ', { err });
      },
    });
  }
}
