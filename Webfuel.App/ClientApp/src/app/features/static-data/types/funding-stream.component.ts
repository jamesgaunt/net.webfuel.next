import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateFundingStream, FundingStream, QueryFundingStream, UpdateFundingStream } from '../../../api/api.types';
import { FundingStreamApi } from '../../../api/funding-stream.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'funding-stream-list',
  templateUrl: '../shared/static-data.component.html'
})
export class FundingStreamComponent extends StaticDataComponent<FundingStream, QueryFundingStream, CreateFundingStream, UpdateFundingStream> {
  constructor(
    dataSource: FundingStreamApi,
  ) {
    super(dataSource);
    this.typeName = "Funding Stream";
  }
}
