import { EventEmitter, Injectable } from '@angular/core';
import _ from 'shared/common/underscore';
import { StaticDataApi } from '../api/static-data.api';
import { FundingBody, IStaticDataModel, Query } from '../api/api.types';
import { BehaviorSubject, filter, switchMap, take } from 'rxjs';
import { IdentityService } from './identity.service';
import { IDataSource } from 'shared/common/data-source';
import { QueryService } from './query.service';

@Injectable()
export class StaticDataService {

  constructor(
    private staticDataApi: StaticDataApi,
    private queryService: QueryService,
    private identityService: IdentityService
  ) {
    this.identityService.identityChanged.subscribe(() => this._loadStaticData());
    this._loadStaticData();
  }

  fundingBodyDataSource: IDataSource<FundingBody> = {
    fetch: (query) => this._fetch(query, s => s.fundingBody),
    changed: new EventEmitter()
  }

  // Implementation

  private _loadingStaticData = false;

  private _staticData = new BehaviorSubject<IStaticDataModel | null>(null);

  private _loadStaticData() {

    if (!this.identityService.isAuthenticated) {
      if (this._staticData.value !== null) {
        this._staticData.next(null);
        console.log("Cleared Static Data");
      }
      return;
    }

    if (this._loadingStaticData)
      return;
    this._loadingStaticData = true;

    // TODO: Handle error case?

    this.staticDataApi.getStaticData().subscribe((staticData) => {
      this._loadingStaticData = false;
      this._staticData.next(staticData);
      console.log("Loaded Static Data: ", { staticData });
    });
  }

  private _fetch<TItem>(query: Query, selector: (staticData: IStaticDataModel) => TItem[]) {

    if (this._staticData.value === null)
      this._loadStaticData();

    return this._staticData.pipe(
      filter(p => p !== null),
      take(1),
      switchMap((p) => this.queryService.fetch(query, selector(p!))));
  }
}
