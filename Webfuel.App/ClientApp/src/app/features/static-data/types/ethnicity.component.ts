import { Component } from '@angular/core';
import { DialogService } from 'core/dialog.service';
import { CreateEthnicity, Ethnicity, QueryEthnicity, UpdateEthnicity } from '../../../api/api.types';
import { EthnicityApi } from '../../../api/ethnicity.api';
import { StaticDataComponent } from '../shared/static-data.component';

@Component({
  selector: 'ethnicity-list',
  templateUrl: '../shared/static-data.component.html'
})
export class EthnicityComponent extends StaticDataComponent<Ethnicity, QueryEthnicity, CreateEthnicity, UpdateEthnicity> {
  constructor(
    dataSource: EthnicityApi,
  ) {
    super(dataSource);
    this.typeName = "Ethnicity";
  }
}
