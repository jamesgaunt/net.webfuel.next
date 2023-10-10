import { EventEmitter, Injectable } from '@angular/core';
import _ from '../shared/underscore';
import { StaticDataApi } from '../api/static-data.api';
import { IStaticDataModel } from '../api/api.types';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class StaticDataService {

  constructor(
    private staticDataApi: StaticDataApi
  ) { }

  get staticData() {
    return this._staticData;
  }
  private _staticData = new BehaviorSubject<IStaticDataModel | null>(null);

  clearStaticData() {
    this._staticData.next(null);
    console.log("Cleared Static Data");
  }

  reloadStaticData() {
    this.staticDataApi.getStaticData().subscribe((result) => {
      this._staticData.next(result);
      console.log("Loaded Static Data");
      console.log(result);
    });
  }
}
