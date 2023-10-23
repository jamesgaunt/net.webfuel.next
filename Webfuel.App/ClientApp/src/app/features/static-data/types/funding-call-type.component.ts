import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateFundingCallType, FundingCallType, QueryFundingCallType, UpdateFundingCallType } from '../../../api/api.types';
import { FundingCallTypeApi } from '../../../api/funding-call-type.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'funding-call-type-list',
  templateUrl: '../shared/static-data.component.html'
})
export class FundingCallTypeComponent extends StaticDataComponent<FundingCallType, QueryFundingCallType, CreateFundingCallType, UpdateFundingCallType> {
  constructor(
    dataSource: FundingCallTypeApi,
  ) {
    super(dataSource);
    this.typeName = "Funding Call Type";
    this.enableHidden = true;
    this.enableFreeText = true;
  }
}
