import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateFundingBody, FundingBody, QueryFundingBody, UpdateFundingBody } from '../../../api/api.types';
import { FundingBodyApi } from '../../../api/funding-body.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'funding-body-list',
  templateUrl: '../shared/static-data.component.html'
})
export class FundingBodyComponent extends StaticDataComponent<FundingBody, QueryFundingBody, CreateFundingBody, UpdateFundingBody> {
  constructor(
    dataSource: FundingBodyApi,
  ) {
    super(dataSource);
    this.typeName = "Funding Body";
  }
}
