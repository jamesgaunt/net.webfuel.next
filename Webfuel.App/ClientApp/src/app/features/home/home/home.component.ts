import { Component, TemplateRef, ViewChild } from '@angular/core';
import { FundingBodyApi } from '../../../api/funding-body.api';
import { IDataSource } from '../../../shared/data-source/data-source';
import { FundingBody } from '../../../api/api.types';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent {
  constructor(
    public fundingBodyApi: FundingBodyApi
  ) {
  }

  dataSource: IDataSource<FundingBody> = {
    fetch: (query) => this.fundingBodyApi.queryFundingBody(query)
  }

  isOpen = false;
}
