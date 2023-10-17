import { EventEmitter, Injectable } from '@angular/core';
import _ from 'shared/common/underscore';
import { StaticDataApi } from '../api/static-data.api';
import { FundingBody, FundingStream, Gender, IStaticDataModel, Query, ResearchMethodology, Title } from '../api/api.types';
import { BehaviorSubject, filter, first, switchMap, take } from 'rxjs';
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

  load<TItem>(query: Query, selector: (staticData: IStaticDataModel) => TItem[]) {

    if (this._staticData.value === null)
      this._loadStaticData();

    return this._staticData.pipe(
      first(p => p !== null), // This is a one-shot subscription
      switchMap((p) => this.queryService.fetch(query, selector(p!))));
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

    this.staticDataApi.getStaticData().subscribe({
      next: (staticData) => {
        this._loadingStaticData = false;
        this._staticData.next(staticData);
        console.log("Loaded Static Data: ", { staticData });
      },
      error: (err) => {
        this._loadingStaticData = false;
        console.log("Error Loading Static Data: ", { err });
      }
    });
  }

}
