import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateHowDidYouFindUs, HowDidYouFindUs, QueryHowDidYouFindUs, UpdateHowDidYouFindUs } from '../../../api/api.types';
import { HowDidYouFindUsApi } from '../../../api/how-did-you-find-us.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'how-did-you-find-us-list',
  templateUrl: '../shared/static-data.component.html'
})
export class HowDidYouFindUsComponent extends StaticDataComponent<HowDidYouFindUs, QueryHowDidYouFindUs, CreateHowDidYouFindUs, UpdateHowDidYouFindUs> {
  constructor(
    dataSource: HowDidYouFindUsApi,
  ) {
    super(dataSource);
    this.typeName = "Comms";
    this.enableFreeText = true;
    this.enableHidden = true;
  }
}
